using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Dialogs;
using WindowsFormsApp1.Packed_And_Ready;
using WindowsFormsApp1.Packed_And_Ready.View_Button;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class PBCMain : Form
    {
        // -----------------------------
        // Fields
        // -----------------------------
        private readonly List<PbJobModel> _pbJobs = new List<PbJobModel>();
        private readonly List<PbJobModel> _pbJobsWShipped = new List<PbJobModel>();
        public event EventHandler<PbJobModel> SoftDeleteRequested;
        private Dictionary<int, PbJobModel> _jobsById = new Dictionary<int, PbJobModel>();
        private System.Windows.Forms.Timer _pollTimer;
        private bool _isPolling;
        public string iniFileName;
        public static bool PausePolling { get; set; }
        internal INIClass? appINI;
        private DateTime? _lastJobsTimestamp;
        private int _lastJobsCount = 0;
        public static CpsConfig DbCpsConfig;
        private bool _isReconnecting = false;
        public static string CPSConnectionString;
        private string _preparedCpsQuery;
        public event EventHandler ItemsChanged;
        private List<PbJobModel> _shipmentRows = new();
        private readonly HashSet<int> _skipNextRefreshJobIds = new HashSet<int>();
        private readonly object _skipLock = new object();
        public static PBCMain Instance { get; private set; }


        private bool _isConnected = false;
        // -----------------------------
        // Constructor
        // -----------------------------
        public PBCMain()
        {
            InitializeComponent();
            Instance = this;
            // UI Setup ONLY
            ApplyTabStyles();
            WireCheckSetToNavigator();
            InitializeTitleBarButtons();

            // UI event wiring
            UiEvents();

            StartPosition = FormStartPosition.Manual;
            var mouseScreen = Screen.FromPoint(Cursor.Position);
            Location = mouseScreen.WorkingArea.Location;
            WindowState = FormWindowState.Maximized;
            lvBuild.EnableDoubleBuffer();
            packedListView2.EnableDoubleBuffer();
            pickedUpListView.EnableDoubleBuffer();
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            SyncNavigatorWithCheckedTab();
            try
            {
                await InitializeAppAsync();
            }
            catch (Exception ex)
            {
                Utils.WriteUnexpectedError("App initialization failed (Form1_Shown)");
                Utils.WriteExceptionError(ex);
                ShowDatabaseError(ex);
            }
        }


        // -----------------------------
        // Loading Configs
        // -----------------------------\
        private async Task InitializeAppAsync()
        {
            await InitializeRqliteAsync();

            if (!await StartApplicationAsync())
                return;

           
        }
        private async Task InitializeRqliteAsync()
        {
            while (true)
            {
                if (!string.IsNullOrWhiteSpace(Program.AppINI._rqClientAddress) &&
                    Program.AppINI._rqClientMaxRetries > 0 &&
                    Program.AppINI._rqClientDelayMs > 0 && Program.AppINI._appRefresh > 0)
                    break;
                var result = MessageDialogBox.ShowDialog(
                    "Configuration Missing",
                    "Rqlite is not configured.\n\nOpen settings now?",
                    MessageBoxButtons.YesNo,
                    MessageType.Warning
                );

                if (result == DialogResult.No)
                    throw new InvalidOperationException("Rqlite configuration missing.");

                using (var dlg = new SettingsDialog())
                {
                    dlg.ShowDialog();
                }

                // reload INI after saving
                Program.AppINI.GetINIVars(out _);
            }

            if (RqliteClient.httpClient == null)
            {
                ServicePointManager.Expect100Continue = false;

                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                var client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(Program.AppINI._rqClientAddress.Trim()),
                    Timeout = TimeSpan.FromSeconds(30)
                };

                RqliteClient.httpClient = client;
                RqliteClient.DefaultEndPoint = Program.AppINI._rqClientAddress.Trim();
            }
        }

        private async Task<bool> StartApplicationAsync()
        {
            if (!await RqliteClient.IsDatabaseAvailableAsync())
            {
                _ = StartReconnectWatcher();
                return false;
            }

            await OnDatabaseConnectedAsync();
            return true;
        }

        private async Task OnDatabaseConnectedAsync()
        {
            _isConnected = true;
            _isReconnecting = false;

            Utils.hideStatusAndSpinner(lbDbConnecting, pbConnectionSpinner, "Connected");

            await LoadJobsAsync();
            _lastJobsTimestamp = await RqliteClient.GetJobsLastUpdatedAsync();

            await LoadCPSConfig(); // 🔥 now included here

            StartBackgroundPolling();
        }
        public static async Task LoadCPSConfig()
        {
            try
            {
                var result = await Utils.LoadCPSConfig();

                DbCpsConfig = result.config;
                CPSConnectionString = result.connectionString;
            }
            catch (Exception ex)
            {
                Utils.WriteUnexpectedError("LoadCPSConfig failed");
                Utils.WriteExceptionError(ex);
                MessageDialogBox.ShowDialog("",
                    $"Cannot load CPS config.\n\n{ex.Message}",
                    MessageBoxButtons.OK,
                    MessageType.Error);
            }
        }

        // -------------------------
        // Polling
        // -------------------------
        private void StartBackgroundPolling()
        {
            _pollTimer?.Stop();
            _pollTimer?.Dispose();

            _pollTimer = new System.Windows.Forms.Timer();
            _pollTimer.Interval = Program.AppINI._appRefresh;

            _pollTimer.Tick += async (_, __) =>
            {
                if (_isPolling) return;
                if (PausePolling) return;

                _isPolling = true;

                try
                {
                    lvBuild.BeginUpdate();
                    packedListView2.BeginUpdate();
                    pickedUpListView.BeginUpdate();

                    await PollForUpdatesAsync();
                }
                catch (Exception ex)
                {
                    Utils.WriteUnexpectedError("Polling failed (StartBackgroundPolling)");

                    Utils.WriteExceptionError(ex);

                    _pollTimer.Stop();
                    _isConnected = false;

                    Utils.showStatusAndSpinner(lbDbConnecting, pbConnectionSpinner, "Database Offline - Reconnecting...");

                    if (!_isReconnecting)
                    {
                        _ = StartReconnectWatcher();
                    }
                }
                finally
                {
                    lvBuild.EndUpdate();
                    packedListView2.EndUpdate();
                    pickedUpListView.EndUpdate();

                    _isPolling = false;
                }
            };

            _pollTimer.Start();
        }


        private async Task PollForUpdatesAsync()
        {
            Debug.WriteLine("Refreshing..");
            if (!await RqliteClient.IsDatabaseAvailableAsync())
            {
                Utils.WriteUnexpectedError("Database unavailable during polling.");
                throw new InvalidOperationException("Database offline");
            }
            if (WindowState == FormWindowState.Minimized)
                return;
            if (Application.OpenForms.OfType<Form>()
     .Any(f => f is ViewWOListDialog))
                return;

            // ⭐ GLOBAL CHANGE DETECTION (very cheap)
            var dbTimestamp = await RqliteClient.GetJobsLastUpdatedAsync();
            var dbCount = await RqliteClient.GetJobsCountAsync();

            // nothing changed
            if (_lastJobsTimestamp == dbTimestamp && dbCount == _lastJobsCount)
            {
                Debug.WriteLine("No updates..");
                return;
            }
               

            _lastJobsTimestamp = dbTimestamp;
            _lastJobsCount = dbCount;

            // ---------------------------------------
            // Existing logic runs only when needed
            // ---------------------------------------

            var dbInfo = await RqliteClient.LoadJobUpdateInfoAsync();

            var dbJobIds = dbInfo.Select(x => x.JobId).ToHashSet();

            // -------------------------
            // 1️⃣ Detect NEW jobs
            // -------------------------
            var newJobIds = dbInfo
     .Select(x => x.JobId)
     .Where(id => !_jobsById.ContainsKey(id))
     .ToList();

            foreach (var jobId in newJobIds)
            {
                var newJob = await RqliteClient.LoadSingleJobGraphAsync(jobId);

                if (newJob != null)
                {
                    _pbJobs.Add(newJob);
                    _jobsById[jobId] = newJob;

                    RefreshSingleJobUI(newJob);
                }
            }

            // -------------------------
            // 2️⃣ Detect UPDATED jobs
            // -------------------------
            foreach (var (jobId, lastUpdated) in dbInfo)
            {
                if (!_jobsById.TryGetValue(jobId, out var local))
                    continue;

                if (local.LastUpdated != lastUpdated)
                {
                    //await RefreshSingleJobAsync(jobId);
                    if (ShouldSkipRefresh(jobId))
                    {
                        Debug.WriteLine($"[SKIP] Job {jobId}");
                        continue; // if inside loop
                    }

                    await RefreshSingleJobAsync(jobId);
                }
            }

            // -------------------------
            // 3️⃣ Detect DELETED jobs
            // -------------------------
            foreach (var local in _pbJobs.ToList())
            {
                if (!dbJobIds.Contains(local.JobId))
                {
                    _pbJobs.Remove(local);
                    _jobsById.Remove(local.JobId);

                    RemoveJobFromUI(local.JobId);
                }
            }
        }


        private async Task StartReconnectWatcher()
        {
            if (_isReconnecting) return;

            _isReconnecting = true;
            _isConnected = false;

            Utils.showStatusAndSpinner(lbDbConnecting, pbConnectionSpinner, "Database Offline - Reconnecting...");

            while (_isReconnecting)
            {
                try
                {
                    Utils.WriteUnexpectedError("Reconnect attempt...");
                    await Task.Delay(3000);

                    if (await RqliteClient.IsDatabaseAvailableAsync())
                    {
                        Utils.WriteUnexpectedError("Database reconnected successfully.");
                        _isReconnecting = false;

                        // Ensure UI thread
                        if (InvokeRequired)
                        {
                            BeginInvoke(new Action(async () =>
                            {
                                await OnDatabaseConnectedAsync();
                            }));
                        }
                        else
                        {
                            await OnDatabaseConnectedAsync();
                        }

                        break;
                    }
                }
                catch (Exception ex)
                {
                    Utils.WriteUnexpectedError("Reconnect attempt (StartReconnectWatcher)");
                }
            }
        }

        private void ShowDatabaseError(Exception ex)
        {

            MessageDialogBox.ShowDialog(
                "Database Error",
                $"Database error:\n\n{ex.Message}",
                MessageBoxButtons.OK,
                MessageType.Error
            );
        }


        // -----------------------------
        // UI Styling / Wiring
        // -----------------------------
        private void UiEvents()
        {

            packedListView2.PackedDataChanged += PackedListView2_PackedDataChanged;
            lvBuild.PalletChanged += PalletListView_PalletChanged;

            //lvBuild.DeleteRequested += async (_, job) =>
            //{
            //    if (job == null) return;

            //    try
            //    {
            //        await RqliteClient.DeletePbJobAsync(job.JobId);
            //        _pbJobs.Remove(job);
            //        _jobsById.Remove(job.JobId);
            //        RemoveJobFromUI(job.JobId);
            //    }
            //    catch (Exception ex)
            //    {
            //        Utils.WriteUnexpectedError("Delete PB Job failed");
            //        Utils.WriteExceptionError(ex);
            //    }
            //};
            lvBuild.DeleteRequested += async (_, job) =>
            {
                if (job == null) return;

                try
                {
                    await RqliteClient.DeletePbJobAsync(job.JobId);

                    _pbJobs.Remove(job);
                    _jobsById.Remove(job.JobId);
                    RemoveJobFromUI(job.JobId);
                }
                catch (Exception ex)
                {
                    Utils.WriteUnexpectedError("Delete PB Job failed");
                    Utils.WriteExceptionError(ex);
                }
            };

            lvBuild.SoftDeleteRequested += async (_, job) =>
            {
                if (job == null) return;

                try
                {
                    await RqliteClient.SoftDeletePbJobAsync(job.JobId);

                    job.IsActive = false;

                    RefreshSingleJobUI(job);
                }
                catch (Exception ex)
                {
                    Utils.WriteUnexpectedError("Soft delete PB Job failed");
                    Utils.WriteExceptionError(ex);
                }
            };

            lvBuild.EditRequested += async (_, job) =>
            {

                using (var dialog = new CreatePBJobDialog(job))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!int.TryParse(dialog.JobNumber, out int jobNumber) || jobNumber <= 0)

                        {
                            MessageDialogBox.ShowDialog("", "Job number must contain digits", MessageBoxButtons.OK, MessageType.Info);
                            return;
                        }

                        var rows = await RqliteClient.UpdatePbJobAsync(
                                job.JobId,
                                dialog.JobName,
                                jobNumber,
                                dialog.IsTemp,
                                job.LastUpdated);
                        if (rows == 0)
                        {

                            MessageDialogBox.ShowDialog(
                                "Update Conflict",
                                "This job was modified by another workstation.\nPlease reopen and try again.",
                                MessageBoxButtons.OK,
                                MessageType.Warning
                            );

                            await RefreshSingleJobAsync(job.JobId);
                            return;
                        }

                        await RefreshSingleJobAsync(job.JobId);
                    }
                }
            };
        }
        private void PalletListView_PalletChanged(object sender, PbJobModel job)
        {
            RefreshAllViews();
        }

        private async void PackedListView2_PackedDataChanged(object sender, PbJobModel job)
        {
            await RefreshSingleJobAsync(job.JobId);
        }
        private void ApplyTabStyles()
        {
            CSSDesign.ApplyTabColors(kcbPickedUp);
            CSSDesign.ApplyTabColors(kcbBuildPallets);
            CSSDesign.ApplyTabColors(kcbPackedReady);
        }

        private void WireCheckSetToNavigator()
        {
            kryptonCheckSet1.CheckButtons.Add(kcbBuildPallets);
            kryptonCheckSet1.CheckButtons.Add(kcbPackedReady);
            kryptonCheckSet1.CheckButtons.Add(kcbPickedUp);

            kryptonCheckSet1.CheckedButtonChanged += (_, __) =>
            {
                nvNavigator.SelectedIndex = kryptonCheckSet1.CheckedIndex;
            };

            kryptonCheckSet1.CheckedButton = kcbBuildPallets;
            SyncNavigatorWithCheckedTab();
        }

        private void SyncNavigatorWithCheckedTab()
        {
            var idx = kryptonCheckSet1.CheckedIndex;
            nvNavigator.SelectedIndex = (idx >= 0) ? idx : 0;
        }

        private void InitializeTitleBarButtons()
        {
            CSSDesign.MakeTitleBarButton(btnMaximize);
            CSSDesign.MakeTitleBarButton(btnMinimize);
            CSSDesign.MakeTitleBarButton(btnClose);
            CSSDesign.MakeTitleBarButton(btnSettings);
        }
        public void MarkSkipRefresh(int jobId)
        {
            lock (_skipLock)
            {
                _skipNextRefreshJobIds.Add(jobId);
            }
        }

        private bool ShouldSkipRefresh(int jobId)
        {
            lock (_skipLock)
            {
                if (_skipNextRefreshJobIds.Contains(jobId))
                {
                    _skipNextRefreshJobIds.Remove(jobId);
                    return true;
                }
            }
            return false;
        }
        // -----------------------------
        // Data Loading (DB → UI)
        // -----------------------------
        private async Task LoadJobsAsync()
        {
            var jobs = await RqliteClient.LoadJobsAsync();
          
            _pbJobs.Clear();
            _pbJobs.AddRange(jobs);

            _jobsById = jobs.ToDictionary(j => j.JobId);

            RefreshAllViews();
        }

        // -----------------------------
        // View Refresh
        // -----------------------------
        private void RefreshSingleJobUI(PbJobModel job)
        {
            RemoveJobFromUI(job.JobId);

            // ✅ Build view (IsActive only)
            if (job.IsActive)
            {
                lvBuild?.AddItem(job);

                // ✅ Packed view MUST match RefreshAllViews logic
                if (job.Pallets.Any(p =>
                    p.State == PalletState.Ready ||
                    p.State == PalletState.Packed_NotReady))
                {
                    packedListView2?.AddItem(job);
                }
            }

            // ✅ Shipment view (history, includes everything)
            if (job.Pallets.Any(p => p.ShippedAt.HasValue))
            {
                pickedUpListView?.AddItem(job);
            }
        }

        private void RemoveJobFromUI(int jobId)
        {
            lvBuild?.RemoveItem(jobId);
            packedListView2?.RemoveItem(jobId);
            pickedUpListView?.RemoveItem(jobId);
        }
        //   private void RefreshAllViews()
        //   {
        //       var activeJobs = _pbJobs.Where(j => j.IsActive).ToList();
        //       var allJobs = _pbJobs; 

        //       lvBuild?.SetItems(activeJobs);

        //       var packedJobs = activeJobs
        //.Select(job =>
        //{
        //    var activePallets = job.Pallets
        //        .Where(p => p.State == PalletState.Ready ||
        //                    p.State == PalletState.Packed_NotReady)
        //        .Select(p => new Pallet
        //        {
        //            PalletId = p.PalletId,
        //            PBJobId = p.PBJobId,
        //            PalletNumber = p.PalletNumber,
        //            PackedAt = p.PackedAt,
        //            ShippedAt = p.ShippedAt,
        //            TrayCount = p.TrayCount,
        //            State = p.State,
        //            WorkOrders = p.WorkOrders
        //                .Select(w => new WorkOrder(w.WorkOrderCode, w.Quantity)
        //                {
        //                    Id = w.Id,
        //                    PalletId = w.PalletId
        //                })
        //                .ToList()
        //        })
        //        .ToList();

        //    if (!activePallets.Any())
        //        return null;

        //    return new PbJobModel
        //    {
        //        JobId = job.JobId,
        //        JobName = job.JobName,
        //        JobNumber = job.JobNumber,
        //        IsTemp = job.IsTemp,
        //        LastUpdated = job.LastUpdated,
        //        Pallets = activePallets
        //    };
        //})
        //.Where(j => j != null)
        //.ToList();

        //       packedListView2?.SetItems(packedJobs);

        //       _shipmentRows = new List<PbJobModel>();
        //       foreach (var job in _pbJobs)
        //       {
        //           var shippedGroups = job.Pallets
        //               .Where(p => p.State == PalletState.Shipped && p.ShippedAt.HasValue)
        //               .GroupBy(p => p.ShippedAt.Value);

        //           foreach (var group in shippedGroups)
        //           {
        //               var shipmentClone = new PbJobModel
        //               {
        //                   JobId = job.JobId,
        //                   JobName = job.JobName,
        //                   JobNumber = job.JobNumber,
        //                   IsTemp = job.IsTemp,
        //                   LastUpdated = job.LastUpdated,
        //                   ShippedDate = group.Key,
        //                   Pallets = group
        //                       .Select(p => new Pallet
        //                       {
        //                           PalletId = p.PalletId,
        //                           PBJobId = p.PBJobId,
        //                           PalletNumber = p.PalletNumber,
        //                           PackedAt = p.PackedAt,
        //                           ShippedAt = p.ShippedAt,
        //                           TrayCount = p.TrayCount,
        //                           State = p.State,
        //                           WorkOrders = p.WorkOrders
        //                               .Select(w => new WorkOrder(w.WorkOrderCode, w.Quantity)
        //                               {
        //                                   Id = w.Id,
        //                                   PalletId = w.PalletId
        //                               })
        //                               .ToList()
        //                       })
        //                       .ToList()
        //               };

        //               _shipmentRows.Add(shipmentClone);
        //           }
        //       }

        //       _shipmentRows = _shipmentRows
        //           .OrderByDescending(s => s.ShippedDate)
        //           .ToList();

        //       pickedUpListView?.SetItems(_shipmentRows);
        //   }
        private void RefreshAllViews()
        {
            // 1️⃣ Active jobs
            var activeJobs = _pbJobs.Where(j => j.IsActive).ToList();

            // 2️⃣ Build view
            lvBuild?.SetItems(activeJobs);

            // 3️⃣ Packed view
            var packedJobs = activeJobs
                .Where(job => job.Pallets.Any(p =>
                    p.State == PalletState.Ready ||
                    p.State == PalletState.Packed_NotReady))
                .Select(job => new PbJobModel
                {
                    JobId = job.JobId,
                    JobName = job.JobName,
                    JobNumber = job.JobNumber,
                    IsTemp = job.IsTemp,
                    LastUpdated = job.LastUpdated,
                    Pallets = job.Pallets
                        .Where(p => p.State == PalletState.Ready ||
                                    p.State == PalletState.Packed_NotReady)
                        .ToList()
                })
                .ToList();

            packedListView2?.SetItems(packedJobs);

            // 4️⃣ Shipment view (ALL jobs, including inactive)
            _shipmentRows = _pbJobs
                .SelectMany(job => job.Pallets
                    .Where(p => p.State == PalletState.Shipped && p.ShippedAt.HasValue)
                    .GroupBy(p => p.ShippedAt.Value)
                    .Select(group => new PbJobModel
                    {
                        JobId = job.JobId,
                        JobName = job.JobName,
                        JobNumber = job.JobNumber,
                        IsTemp = job.IsTemp,
                        LastUpdated = job.LastUpdated,
                        ShippedDate = group.Key,
                        Pallets = group.ToList()
                    })
                )
                .OrderByDescending(s => s.ShippedDate)
                .ToList();

            pickedUpListView?.SetItems(_shipmentRows);
        }


        private async Task RefreshSingleJobAsync(int jobId)
        {
            var updated = await RqliteClient.LoadSingleJobGraphAsync(jobId);
            if (updated == null)
                return;

            if (!_jobsById.TryGetValue(jobId, out var existing))
                return;

            // Determine shipped state based on pallets
            bool wasShipped = existing.Pallets.Any() &&
                   existing.Pallets.All(p => p.State == PalletState.Shipped);

            bool isShipped = updated.Pallets.Any() &&
                             updated.Pallets.All(p => p.State == PalletState.Shipped);

            // Update model properties
            existing.JobName = updated.JobName;
            existing.JobNumber = updated.JobNumber;
            existing.IsTemp = updated.IsTemp;
            existing.LastUpdated = updated.LastUpdated;
            existing.Pallets = updated.Pallets;
            existing.ShippedDate = updated.Pallets
    .Where(p => p.State == PalletState.Shipped)
    .Select(p => p.ShippedAt)
    .FirstOrDefault();

            _jobsById[jobId] = existing;

            // If shipped state changed → move job between views
            if (wasShipped != isShipped)
            {
                RefreshSingleJobUI(existing);
                return;
            }
            RefreshAllViews();
        }


        // -----------------------------
        // Window Buttons
        // -----------------------------
        private void btnClose_Click(object sender, EventArgs e) => Close();
        private void btnMinimize_Click_1(object sender, EventArgs e)
            =>
            WindowState = FormWindowState.Minimized;
        private void btnMaximize_Click_1(object sender, EventArgs e)
        {
            WindowState = (WindowState == FormWindowState.Maximized)
                ? FormWindowState.Normal
                : FormWindowState.Maximized;
        }

        // -----------------------------
        // Actions
        // -----------------------------
        private async void btnShipPallets_Click(object sender, EventArgs e)
        {
            var selectedJobs = packedListView2.GetReadyJobs().ToList();

            if (!selectedJobs.Any())
            {
                //MessageBox.Show("No jobs selected.");
                MessageDialogBox.ShowDialog("", "No jobs selected.", MessageBoxButtons.OK, MessageType.Info);
                return;
            }

            var jobIds = selectedJobs
                .Select(j => j.JobId)
                .ToArray();

            // Show custom confirmation dialog
            using (var dlg = new ShipPalletsConfirmationDialog())
            {
                if (dlg.ShowDialog(this) != DialogResult.OK)
                {
                    return; // ❗ STOP if user cancels
                }
            }

            try
            {
                await RqliteClient.ShipPalletsAsync(jobIds);
                await LoadJobsAsync();
            }
            catch (Exception ex)
            {
                Utils.WriteUnexpectedError(
    $"Ship pallets | Count={jobIds.Length}, JobIds={string.Join(",", jobIds)}"
);
                ShowDatabaseError(ex);
            }
        }

        private void ApplySearchFilter()
        {
            if (_shipmentRows.Count == 0)
            {
                pickedUpListView.SetItems(new List<PbJobModel>());
                return;
            }

            var fromDate = dtPickUpFrom.Value.Date;
            var toDate = dtPickUpTo.Value.Date.AddDays(1);

            var filtered = _shipmentRows
                .Where(x => x.ShippedDate.HasValue &&
                            x.ShippedDate.Value >= fromDate &&
                            x.ShippedDate.Value < toDate)
                .ToList();

            pickedUpListView.SetItems(filtered);
        }

        private async void btnAddPBJob_Click(object sender, EventArgs e)
        {
            using (var dlg = new CreatePBJobDialog())
            {
                if (dlg.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    var jobNumberText = dlg.JobNumber?.Trim();

                    if (string.IsNullOrWhiteSpace(jobNumberText) || !jobNumberText.All(char.IsDigit))
                    {
                        MessageDialogBox.ShowDialog("", "Job number must contain digits only.", MessageBoxButtons.OK, MessageType.Info);
                        return;
                    }

                    int jobNumber = int.Parse(jobNumberText);

                    var existingJob = await RqliteClient.GetJobByNumberAsync(jobNumber);

                    if (existingJob != null)
                    {
                        // ❌ Active duplicate
                        if (existingJob.IsActive)
                        {
                            MessageDialogBox.ShowDialog(
                                "Duplicate Job",
                                $"Job number {jobNumber} already exists.",
                                MessageBoxButtons.OK,
                                MessageType.Warning
                            );
                            return;
                        }

                        // 🔄 Inactive → Reactivate
                        var confirm = MessageDialogBox.ShowDialog(
                            "Duplicate Found",
                            $"Job number {jobNumber} already exists and contains previous shipments.\n\n" +
                            $"Do you want to reactivate it?",
                            MessageBoxButtons.YesNo,
                            MessageType.Info
                        );

                        if (confirm == DialogResult.Yes)
                        {
                            await RqliteClient.ReactivatePbJobAsync(existingJob.JobId);

                            await LoadJobsAsync(); // 🔥 safer than RefreshSingleJobAsync

                            MessageDialogBox.ShowDialog(
                                "Reactivated",
                                "Job has been restored.",
                                MessageBoxButtons.OK,
                                MessageType.Info
                            );

                            return;
                        }

                        return;
                    }

                    var job = new PbJobModel
                    {
                        JobName = dlg.JobName ?? string.Empty,
                        JobNumber = jobNumber,
                        IsTemp = dlg.IsTemp
                    };

                    Utils.WriteUnexpectedError(
                        $"Create PB Job | JobNumber={jobNumber}, JobName={job.JobName}, IsTemp={job.IsTemp}"
                    );

                    await RqliteClient.InsertPbJobAsync(job);

                    await LoadJobsAsync();
                }
                catch (Exception ex)
                {
                    Utils.WriteUnexpectedError("Create PB Job failed");
                    Utils.WriteExceptionError(ex);
                    ShowDatabaseError(ex);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplySearchFilter();
        }


        private void chkbxSelectAll_CheckedChanged_1(object sender, EventArgs e)
        {
            packedListView2.SetAllSelected(chkbxSelectAll.Checked);
        }

        private async void btnSettings_Click(object sender, EventArgs e)
        {
            using (var dlg = new SettingsDialog())
            {
                dlg.ShowDialog(this);

                if (!dlg.SettingsChanged)
                    return;
            }

            // reload configs
            await InitializeRqliteAsync();
            await LoadCPSConfig();
            StartBackgroundPolling();
        }

        private void cbPackedSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            pickedUpListView.SetAllSelected(cbPackedSelectAll.Checked);
        }

        private async void btnPrintSummary_Click(object sender, EventArgs e)
        {
            var selectedJobs = pickedUpListView.GetSelectedJobs().ToList();

            if (!selectedJobs.Any())
            {
                MessageDialogBox.ShowDialog(
                    "No Selection",
                    "Select at least one row to print.",
                    MessageBoxButtons.OK,
                    MessageType.Info
                );
                return;
            }

            try
            {
                PrintEngine.Print(e => PrintLayouts.DrawSummary(e, selectedJobs));
            }
            catch (Exception ex)
            {
                Utils.WriteUnexpectedError("Print Summary failed");
                Utils.WriteExceptionError(ex);
                MessageDialogBox.ShowDialog(
                    "Printing Error",
                    ex.Message,
                    MessageBoxButtons.OK,
                    MessageType.Error
                );
            }
        }

        private void cbGenerateReport_Click(object sender, EventArgs e)
        {
            var jobs = pickedUpListView.GetAllJobs().ToList();

            if (!jobs.Any())
            {
                MessageDialogBox.ShowDialog(
                    "No Data",
                    "No data available for the current filter.",
                    MessageBoxButtons.OK,
                    MessageType.Info
                );
                return;
            }
            Utils.GenerateReport(jobs, dtPickUpFrom, dtPickUpTo);
        }
    }
}

public static class ControlExtensions
{
    public static void EnableDoubleBuffer(this Control control)
    {
        typeof(Control)
            .GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance)
            .SetValue(control, true, null);
    }
}
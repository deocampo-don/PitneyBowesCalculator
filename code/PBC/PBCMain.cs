using Krypton.Toolkit;
using System;
using System.Collections.Generic;

using System.Diagnostics;

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using PitneyBowesCalculator.Dialogs;
using System.Drawing;
using PitneyBowesCalculator.Packed_And_Ready.View_Button;
using PitneyBowesCalculator;


namespace PitneyBowesCalculator
{
    public partial class PBCMain : Form
    {// -----------------------------
     // Data
     // -----------------------------
        private readonly List<PbJobModel> _pbJobs = new();
        private Dictionary<int, PbJobModel> _jobsById = new();
        private List<PbJobModel> _shipmentRows = new();
        private bool _isShipping = false;

        // -----------------------------
        // Polling
        // -----------------------------
        private System.Windows.Forms.Timer _pollTimer;
        private bool _isPolling;
        private string _lastJobsTimestamp;
        private int _lastJobsCount = 0;

        // -----------------------------
        // Concurrency (NEW SYSTEM)
        // -----------------------------
        private Dictionary<int, string> _pendingUpdates = new();
        private readonly object _pendingLock = new object();
        public static PBCMain Instance { get; private set; }
        public static CpsConfig? DbCpsConfig { get; private set; }
        public static string? CPSConnectionString { get; private set; }
        private Dictionary<int, string> _lastProcessedUpdates = new();

        // -----------------------------
        // Connection
        // -----------------------------
        private bool _isConnected = false;
        private bool _isReconnecting = false;
        private readonly Dictionary<int, Action> _staleCallbacks = new();
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
            if (_isShipping) return;
            if (!await RqliteClient.IsDatabaseAvailableAsync())
                throw new InvalidOperationException("Database offline");


            var dbTimestamp = await RqliteClient.GetJobsLastUpdatedAsync();
            var dbCount = await RqliteClient.GetJobsCountAsync();

            

            if (_lastJobsTimestamp == dbTimestamp && dbCount == _lastJobsCount)
            {
             
                return;
            }
            _lastJobsTimestamp = dbTimestamp;
            _lastJobsCount = dbCount;

            var dbInfo = await RqliteClient.LoadJobUpdateInfoAsync();
  
            var dbJobIds = dbInfo.Select(x => x.JobId).ToHashSet();

            // -------------------------
            // 1️⃣ NEW JOBS
            // -------------------------
            var newJobIds = dbInfo
                .Select(x => x.JobId)
                .Where(id => !_jobsById.ContainsKey(id))
                .ToList();

            foreach (var jobId in newJobIds)
            {
                try
                {
                    var newJob = await RqliteClient.LoadSingleJobGraphAsync(jobId);

                    if (newJob != null)
                    {
                        _pbJobs.Add(newJob);
                        _jobsById[jobId] = newJob;
                        
                        RefreshSingleJobUI(newJob);
                    }
                }
                catch (Exception ex)
                {
                    Utils.WriteUnexpectedError($"Polling NEW job failed | JobId={jobId}");
                    Utils.WriteExceptionError(ex);
                }
            }


            foreach (var (jobId, lastUpdatedRaw) in dbInfo)
            {
                try
                {
                    if (!_jobsById.TryGetValue(jobId, out var local))
                        continue;

                    if (local.LastUpdatedRaw != lastUpdatedRaw)
                    {
                        if (ShouldIgnoreOwnUpdate(jobId, lastUpdatedRaw))
                        {
                            _lastProcessedUpdates[jobId] = lastUpdatedRaw;
                            local.LastUpdatedRaw = lastUpdatedRaw;
                            continue;
                        }

                        // ✅ Only notify stale if pallet structure changed
                        // Work order additions should NOT trigger the banner
                        if (_staleCallbacks.ContainsKey(jobId))
                        {
                            var freshForCheck = await RqliteClient.LoadSingleJobGraphAsync(jobId);

                            if (freshForCheck != null && HasPalletStateChanged(jobId, freshForCheck))
                            {
                                if (_staleCallbacks.TryGetValue(jobId, out var callback))
                                    callback?.Invoke();
                            }

                            // ✅ Pass the already-loaded fresh job to avoid double fetch
                            if (freshForCheck != null)
                            {
                                if (!_jobsById.TryGetValue(jobId, out var existing))
                                    return;

                                existing.JobName = freshForCheck.JobName;
                                existing.JobNumber = freshForCheck.JobNumber;
                                existing.IsTemp = freshForCheck.IsTemp;
                                existing.IsActive = freshForCheck.IsActive;
                                existing.LastUpdated = freshForCheck.LastUpdated;
                                existing.LastUpdatedRaw = freshForCheck.LastUpdatedRaw;
                                existing.Pallets = freshForCheck.Pallets;
                                existing.ShippedDate = freshForCheck.Pallets
                                    .Where(p => p.State == PalletState.Shipped)
                                    .Select(p => p.ShippedAt)
                                    .FirstOrDefault();

                                _jobsById[jobId] = existing;
                                RefreshSingleJobUI(existing);
                            }
                        }
                        else
                        {
                            await RefreshSingleJobAsync(jobId);
                        }

                        _lastProcessedUpdates[jobId] = lastUpdatedRaw;
                    }
                }
                catch (Exception ex)
                {
                    Utils.WriteUnexpectedError($"Polling UPDATE failed | JobId={jobId}");
                    Utils.WriteExceptionError(ex);
                }
            }

            // -------------------------
            // 3️⃣ DELETED JOBS
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

            if (_lastProcessedUpdates.Count > 500)
                _lastProcessedUpdates.Clear();
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
                                try
                                {
                                    await OnDatabaseConnectedAsync();
                                }
                                catch (Exception ex)
                                {
                                    Utils.WriteUnexpectedError("OnDatabaseConnectedAsync failed after reconnect");
                                    Utils.WriteExceptionError(ex);
                                    ShowDatabaseError(ex);
                                }
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

            lvBuild.DeleteRequested += async (_, job) =>
            {
                if (!EnsureConnected()) return;
                if (job == null || _isPolling) return;
              

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
                if (!EnsureConnected()) return;
                if (job == null) return;

                try
                {
                    await RqliteClient.SoftDeletePbJobAsync(job.JobId);

                    // ✅ Get exact timestamp after write
                    var fresh = await RqliteClient.LoadSingleJobGraphAsync(job.JobId);
                    if (fresh?.LastUpdatedRaw != null)
                        MarkPendingUpdate(job.JobId, fresh.LastUpdatedRaw);

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
                if (!EnsureConnected()) return;
                using (var dialog = new CreatePBJobDialog(job))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!int.TryParse(dialog.JobNumber, out int jobNumber) || jobNumber <= 0)
                        {
                            MessageDialogBox.ShowDialog(
                                "",
                                "Job number must contain digits",
                                MessageBoxButtons.OK,
                                MessageType.Info
                            );
                            return;
                        }
                        // 🔥 REFRESH LATEST TIMESTAMP BEFORE UPDATE
                        var fresh = await RqliteClient.LoadSingleJobGraphAsync(job.JobId);
                        var expectedLastUpdated = fresh?.LastUpdatedRaw ?? job.LastUpdatedRaw;
                        var (rows, newLastUpdated) = await RqliteClient.UpdatePbJobAsync(
      job.JobId,
      dialog.JobName,
      jobNumber,
      dialog.IsTemp,
      expectedLastUpdated
  );
                    
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

                        job.JobName = dialog.JobName;
                        job.JobNumber = jobNumber;
                        job.IsTemp = dialog.IsTemp;
                        job.LastUpdatedRaw = newLastUpdated;
                        job.LastUpdated = RqliteClient.ParseUtc(newLastUpdated);
                        MarkPendingUpdate(job.JobId, newLastUpdated);
                        RefreshSingleJobUI(job);

                     
                    }
                }
            };
        }
        private void PalletListView_PalletChanged(object sender, PbJobModel job)
        {
            RefreshSingleJobUI(job);
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
        // -----------------------------
        // Data Loading (DB → UI)
        // -----------------------------
        private async Task LoadJobsAsync()
        {
            try
            {
                var jobs = await RqliteClient.LoadJobsAsync();

                _pbJobs.Clear();
                _pbJobs.AddRange(jobs);

                _jobsById = jobs.ToDictionary(j => j.JobId);

                RefreshAllViews();
                _lastProcessedUpdates.Clear();
            }
            catch (Exception ex)
            {
                Utils.WriteUnexpectedError("LoadJobsAsync failed");
                Utils.WriteExceptionError(ex);
                throw; // keep this so startup knows it failed
            }
        }

        // -----------------------------
        // View Refresh
        // -----------------------------
        private void RefreshSingleJobUI(PbJobModel job)
        {
            int buildIndex = lvBuild?.GetItemIndex(job.JobId) ?? -1;
            int packedIndex = packedListView2?.GetItemIndex(job.JobId) ?? -1;

            // ✅ Save scroll positions before any changes
            int buildScroll = lvBuild?.GetScrollPosition() ?? 0;
            int packedScroll = packedListView2?.GetScrollPosition() ?? 0;

            // Remove from all 3 views
            lvBuild?.RemoveItem(job.JobId);
            packedListView2?.RemoveItem(job.JobId);
            pickedUpListView?.RemoveItemsByJobId(job.JobId);

            // Build view
            if (job.IsActive)
            {
                if (buildIndex >= 0)
                {
                    lvBuild?.InsertItem(job, buildIndex);
                    lvBuild?.SetScrollPosition(buildScroll);
                }
                else
                    lvBuild?.AddItem(job);
            }

            // ✅ Restore build scroll position
            if (buildIndex >= 0)
                lvBuild?.SetScrollPosition(buildScroll);

            // Packed view
            var packedPallets = job.Pallets
                .Where(p => p.State == PalletState.Ready ||
                            p.State == PalletState.Packed_NotReady)
                .ToList();

            if (job.IsActive && packedPallets.Any())
            {
                var packedJob = new PbJobModel
                {
                    JobId = job.JobId,
                    JobName = job.JobName,
                    JobNumber = job.JobNumber,
                    IsTemp = job.IsTemp,
                    LastUpdated = job.LastUpdated,
                    LastUpdatedRaw = job.LastUpdatedRaw,
                    Pallets = packedPallets
                };

                if (packedIndex >= 0)
                    packedListView2?.InsertItem(packedJob, packedIndex);
                else
                    packedListView2?.AddItem(packedJob);
            }

            // ✅ Restore packed scroll position
            if (packedIndex >= 0)
                packedListView2?.SetScrollPosition(packedScroll);

            // Shipment view
            var shipmentRows = job.Pallets
                .Where(p => p.State == PalletState.Shipped && p.ShippedAt.HasValue)
                .GroupBy(p => new { p.ShippedAt.Value, p.JobNameSnapshot })
                .Select(group => new PbJobModel
                {
                    JobId = job.JobId,
                    JobName = group.Key.JobNameSnapshot,
                    JobNumber = job.JobNumber,
                    IsTemp = job.IsTemp,
                    LastUpdated = job.LastUpdated,
                    ShippedDate = group.Key.Value,
                    Pallets = group.ToList()
                })
                .ToList();

            foreach (var row in shipmentRows)
                pickedUpListView?.AddItem(row);

            pickedUpListView?.SortByShippedDateDescending();

            _shipmentRows.RemoveAll(r => r.JobId == job.JobId);
            _shipmentRows.AddRange(shipmentRows);
            _shipmentRows = _shipmentRows.OrderByDescending(s => s.ShippedDate).ToList();
        }

        private void RemoveJobFromUI(int jobId)
        {
            lvBuild?.RemoveItem(jobId);
            packedListView2?.RemoveItem(jobId);
            pickedUpListView?.RemoveItemsByJobId(jobId);
        }
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
                    LastUpdatedRaw = job.LastUpdatedRaw,
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
                    .GroupBy(p => new { p.ShippedAt.Value, p.JobNameSnapshot })
                    .Select(group => new PbJobModel
                    {
                        JobId = job.JobId,
                        JobName = group.Key.JobNameSnapshot,
                        JobNumber = job.JobNumber,
                        IsTemp = job.IsTemp,
                        LastUpdated = job.LastUpdated,
                        LastUpdatedRaw = job.LastUpdatedRaw,
                        ShippedDate = group.Key.Value,
                        Pallets = group.ToList()
                    })
                )
                .OrderByDescending(s => s.ShippedDate)
                .ToList();

            pickedUpListView?.SetItems(_shipmentRows);
        }

     
        public async Task RefreshSingleJobAsync(int jobId)
        {
            var updated = await RqliteClient.LoadSingleJobGraphAsync(jobId);

            if (updated == null)
                return;

            if (!_jobsById.TryGetValue(jobId, out var existing))
                return;

            existing.JobName = updated.JobName;
            existing.JobNumber = updated.JobNumber;
            existing.IsTemp = updated.IsTemp;
            existing.IsActive = updated.IsActive;
            existing.LastUpdated = updated.LastUpdated;
            existing.LastUpdatedRaw = updated.LastUpdatedRaw;
            existing.Pallets = updated.Pallets;
            existing.ShippedDate = existing.Pallets
                .Where(p => p.State == PalletState.Shipped)
                .Select(p => p.ShippedAt)
                .FirstOrDefault();

            _jobsById[jobId] = existing;

            RefreshSingleJobUI(existing);
        }
        public void MarkPendingUpdate(int jobId, string timestamp)
        {
            if (string.IsNullOrEmpty(timestamp))
                return;

            lock (_pendingLock)
            {
                _pendingUpdates[jobId] = timestamp;
            }
        }

   
        private bool ShouldIgnoreOwnUpdate(int jobId, string dbTimestamp)
        {
            if (dbTimestamp == null)
                return false;

            lock (_pendingLock)
            {
                if (_pendingUpdates.TryGetValue(jobId, out var pending))
                {
                    if (pending == dbTimestamp)
                    {
                        _pendingUpdates.Remove(jobId); // consume once
                        return true;
                    }
                }
            }

            return false;
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

            if (!EnsureConnected()) return;
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

                    // =========================
                    // HANDLE EXISTING JOB
                    // =========================
                    if (existingJob != null)
                    {
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

                        var confirm = MessageDialogBox.ShowDialog(
                            "Duplicate Found",
                            $"Job number {jobNumber} already exists and contains previous shipments.\n\n" +
                            $"Do you want to reactivate it?",
                            MessageBoxButtons.YesNo,
                            MessageType.Info
                        );

                        if (confirm != DialogResult.Yes)
                            return;

                        var rename = MessageDialogBox.ShowDialog(
                            "Rename Job",
                            "Would you like to rename the reused PB Job?",
                            MessageBoxButtons.YesNo,
                            MessageType.Info);

                        if (rename == DialogResult.Yes)
                        {
                            using (var renameDlg = new CreatePBJobDialog(existingJob))
                            {
                                if (renameDlg.ShowDialog(this) != DialogResult.OK)
                                    return;

                                var newName = renameDlg.JobName;

                                if (string.IsNullOrWhiteSpace(newName))
                                {
                                    MessageDialogBox.ShowDialog(
                                        "",
                                        "Job name cannot be empty.",
                                        MessageBoxButtons.OK,
                                        MessageType.Warning);
                                    return;
                                }

                                await RqliteClient.ReactivatePbJobAndRenameAsync(
                                    existingJob.JobId,
                                    newName
                                );
                            }
                        }
                        else
                        {
                            await RqliteClient.ReactivatePbJobAsync(existingJob.JobId);
                        }

                        // ✅ Load fresh state for this job only
                        var reactivated = await RqliteClient.LoadSingleJobGraphAsync(existingJob.JobId);

                        if (reactivated != null)
                        {
                            // ✅ Update in-memory collections
                            if (_jobsById.ContainsKey(reactivated.JobId))
                            {
                                _pbJobs.Remove(_jobsById[reactivated.JobId]);
                            }

                            _pbJobs.Add(reactivated);
                            _jobsById[reactivated.JobId] = reactivated;

                            // ✅ Suppress poll cycle for this job
                            MarkPendingUpdate(reactivated.JobId, reactivated.LastUpdatedRaw);

                            // ✅ Single job UI refresh
                            RefreshSingleJobUI(reactivated);
                        }

                        MessageDialogBox.ShowDialog(
                            "Reactivated",
                            "Job has been restored.",
                            MessageBoxButtons.OK,
                            MessageType.Info
                        );

                        return;
                    }

                    // =========================
                    // CREATE NEW JOB
                    // =========================
                    var job = new PbJobModel
                    {
                        JobName = dlg.JobName ?? string.Empty,
                        JobNumber = jobNumber,
                        IsTemp = dlg.IsTemp
                    };

                    Utils.WriteUnexpectedError(
                        $"Create PB Job | JobNumber={jobNumber}, JobName={job.JobName}, IsTemp={job.IsTemp}"
                    );

                    int newJobId = await RqliteClient.InsertPbJobAsync(job);

                    // ✅ Load fresh state for the new job only
                    var newJob = await RqliteClient.LoadSingleJobGraphAsync(newJobId);

                    if (newJob != null)
                    {
                        _pbJobs.Add(newJob);
                        _jobsById[newJob.JobId] = newJob;

                        // ✅ Suppress poll cycle for this job
                        MarkPendingUpdate(newJob.JobId, newJob.LastUpdatedRaw);

                        // ✅ Single job UI refresh
                        RefreshSingleJobUI(newJob);
                    }
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


        private async void btnShipPallets_Click_1(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            try
            {
                var selectedJobs = packedListView2.GetReadyJobs().ToList();

                if (!selectedJobs.Any())
                {
                    MessageDialogBox.ShowDialog("", "No jobs selected.", MessageBoxButtons.OK, MessageType.Info);
                    return;
                }

                var jobIds = selectedJobs.Select(j => j.JobId).ToArray();

                // 🔥 SNAPSHOT BEFORE DIALOG
                var originalSnapshot = selectedJobs.ToDictionary(
                    j => j.JobId,
                    j => j.LastUpdatedRaw
                );

                using (var dlg = new ShipPalletsConfirmationDialog())
                {
                    if (dlg.ShowDialog(this) != DialogResult.OK)
                        return;
                }

                // ✅ Reload only selected jobs instead of full LoadJobsAsync
                var reloadedJobs = new List<PbJobModel>();

                foreach (var id in jobIds)
                {
                    var fresh = await RqliteClient.LoadSingleJobGraphAsync(id);
                    if (fresh == null) continue;

                    // ✅ Update in-memory state
                    if (_jobsById.TryGetValue(fresh.JobId, out var existing))
                    {
                        existing.LastUpdatedRaw = fresh.LastUpdatedRaw;
                        existing.Pallets = fresh.Pallets;
                    }

                    reloadedJobs.Add(fresh);
                }

                // ✅ Re-derive selected jobs from reloaded data
                selectedJobs = reloadedJobs
                    .Where(j => j.Pallets.Any(p =>
                        p.State == PalletState.Ready &&
                        p.PackedAt.HasValue))
                    .ToList();

                if (!selectedJobs.Any())
                {
                    MessageDialogBox.ShowDialog(
                        "",
                        "Selected jobs are no longer available or already processed.",
                        MessageBoxButtons.OK,
                        MessageType.Info
                    );
                    return;
                }

                jobIds = selectedJobs.Select(j => j.JobId).ToArray();

                // 🔥 DETECT DATA CHANGES
                var changedJobs = new List<int>();

                foreach (var job in selectedJobs)
                {
                    if (!originalSnapshot.TryGetValue(job.JobId, out var originalTs))
                        continue;

                    if (job.LastUpdatedRaw != originalTs)
                        changedJobs.Add(job.JobId);
                }

                if (changedJobs.Any())
                {
                    var msg =
                        "Some selected jobs were modified by another workstation:\n\n" +
                        string.Join("\n", changedJobs.Select(id => $"Job ID: {id}")) +
                        "\n\nDo you want to continue anyway?";

                    var result = MessageDialogBox.ShowDialog(
                        "Data Changed",
                        msg,
                        MessageBoxButtons.YesNo,
                        MessageType.Warning
                    );

                    if (result != DialogResult.Yes)
                        return;
                }

                // 🔥 UI LOADING STATE
                lbPrintShip.Visible = true;
                lbPrintShip.Text = "Preparing...";
                progressBarShip.Visible = true;
                progressBarShip.Style = ProgressBarStyle.Marquee;

                _isShipping = true;
                Cursor.Current = Cursors.WaitCursor;
               
                this.Enabled = false;

                // 🔥 DB UPDATE
                await RqliteClient.ShipPalletsAsync(jobIds);

                // ✅ Reload fresh data + mark pending in one pass
                var freshJobs = new List<PbJobModel>();

                foreach (var id in jobIds)
                {
                    var fresh = await RqliteClient.LoadSingleJobGraphAsync(id);
                    if (fresh == null) continue;

                    freshJobs.Add(fresh);

                    // ✅ Suppress poll cycle for each shipped job
                    if (fresh.LastUpdatedRaw != null)
                        MarkPendingUpdate(id, fresh.LastUpdatedRaw);
                }

                lbPrintShip.Text = "Generating PDF...";

                await Task.Run(() =>
                {
                    PrintEngine.Print(e => PrintLayouts.SummaryShip(e, freshJobs));
                });

                // ✅ Final UI sync using already-loaded fresh data
                foreach (var fresh in freshJobs)
                {
                    if (_jobsById.TryGetValue(fresh.JobId, out var existing))
                    {
                        existing.JobName = fresh.JobName;
                        existing.JobNumber = fresh.JobNumber;
                        existing.IsTemp = fresh.IsTemp;
                        existing.LastUpdated = fresh.LastUpdated;
                        existing.LastUpdatedRaw = fresh.LastUpdatedRaw;
                        existing.Pallets = fresh.Pallets;
                        existing.ShippedDate = fresh.Pallets
                            .Where(p => p.State == PalletState.Shipped)
                            .Select(p => p.ShippedAt)
                            .FirstOrDefault();

                        _jobsById[existing.JobId] = existing;
                        RefreshSingleJobUI(existing);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.WriteUnexpectedError(
                    $"Ship pallets | JobIds={string.Join(",", packedListView2.GetReadyJobs().Select(j => j.JobId))}"
                );
                ShowDatabaseError(ex);
            }
            finally
            {
                lbPrintShip.Visible = false;
                progressBarShip.Visible = false;
                _isShipping = false;
                Cursor.Current = Cursors.Default;
                this.Enabled = true;
                progressBarShip.Style = ProgressBarStyle.Blocks;
                progressBarShip.Value = 0;
              
            }
        }

        private void chkbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            packedListView2.SetAllSelected(chkbxSelectAll.Checked);
        }

        public void RegisterStaleCallback(int jobId, Action callback)
    => _staleCallbacks[jobId] = callback;

        public void UnregisterStaleCallback(int jobId)
            => _staleCallbacks.Remove(jobId);
        private bool HasPalletStateChanged(int jobId, PbJobModel updated)
        {
            if (!_jobsById.TryGetValue(jobId, out var existing))
                return false;

            // Compare pallet count
            if (existing.Pallets.Count != updated.Pallets.Count)
                return true;

            // Compare each pallet's state and PackedAt
            foreach (var updatedPallet in updated.Pallets)
            {
                var existingPallet = existing.Pallets
                    .FirstOrDefault(p => p.PalletId == updatedPallet.PalletId);

                if (existingPallet == null)
                    return true; // new pallet appeared

                if (existingPallet.State != updatedPallet.State)
                    return true; // state changed (packed, unpacked, shipped)

                if (existingPallet.PackedAt != updatedPallet.PackedAt)
                    return true; // packed state changed
            }

            return false;
        }

        public bool EnsureConnected()
        {
            if (!_isConnected)
            {
                MessageDialogBox.ShowDialog(
                    "Database Offline",
                    "Please wait for reconnection.",
                    MessageBoxButtons.OK,
                    MessageType.Warning
                );
                return false;
            }
            return true;
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
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Packed_And_Ready;
using WindowsFormsApp1.Packed_And_Ready.View_Button;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        // -----------------------------
        // Fields
        // -----------------------------
        private readonly List<PbJobModel> _pbJobs = new List<PbJobModel>();
        private Dictionary<int, PbJobModel> _jobsById = new Dictionary<int, PbJobModel>();
        private System.Windows.Forms.Timer _pollTimer;
        private bool _isPolling;
        public string iniFileName;
        internal INIClass? appINI;
        private DateTime? _lastJobsTimestamp;

        string CPSConnectionString = string.Empty;

        public event EventHandler ItemsChanged;

        // -----------------------------
        // Constructor
        // -----------------------------
        public Main()
        {
            InitializeComponent();
         

            // UI Setup ONLY
            ApplyTabStyles();
            WireCheckSetToNavigator();
            InitializeTitleBarButtons();


            // UI event wiring
            packedListView2.PackedDataChanged += PackedListView2_PackedDataChanged;
            lvBuild.PalletChanged += PalletListView_PalletChanged;

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
                    ShowDatabaseError(ex);
                }
            };

            lvBuild.EditRequested += async (_, job) =>
            {
                using (var dialog = new CreatePBJobDialog(job))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!int.TryParse(dialog.JobNumber, out int jobNumber))
                        {
                            MessageBox.Show("Invalid job number.");
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
                            MessageBox.Show(
                                "This job was modified by another workstation.\nPlease reopen and try again.",
                                "Update Conflict",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            await RefreshSingleJobAsync(job.JobId);
                            return;
                        }

                        await RefreshSingleJobAsync(job.JobId);
                    }
                }
            };

            StartPosition = FormStartPosition.Manual;

            var mouseScreen = Screen.FromPoint(Cursor.Position);

            this.Location = mouseScreen.WorkingArea.Location;

            this.WindowState = FormWindowState.Maximized;
            lvBuild.EnableDoubleBuffer();
            packedListView2.EnableDoubleBuffer();
            pickedUpListView.EnableDoubleBuffer();
        }

        // -----------------------------
        // ⭐ rqlite Initialization
        // -----------------------------
        private async Task InitializeRqliteAsync()
        {
            if (RqliteClient.httpClient == null)
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                RqliteClient.httpClient = new HttpClient(handler);

                // 🔴 Use HTTPS and your server IP
                //RqliteClient.DefaultEndPoint = "https://10.32.101.160:4001";
                RqliteClient.DefaultEndPoint = "http://127.0.0.1:4001";
            }

            await RqliteClient.CreatePbSchemaAsync();
        }

        //keydown event

        private void StartBackgroundPolling()
        {
            _pollTimer = new System.Windows.Forms.Timer();
            _pollTimer.Interval = 5000; // 5 seconds

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
                catch
                {
                    // optional logging
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
            if (WindowState == FormWindowState.Minimized)
                return;
            if (Application.OpenForms.OfType<ViewButtonDialog>().Any())
                return;

            // ⭐ GLOBAL CHANGE DETECTION (very cheap)
            var dbTimestamp = await RqliteClient.GetJobsLastUpdatedAsync();

            if (_lastJobsTimestamp == dbTimestamp)
                return; // nothing changed in the database

            _lastJobsTimestamp = dbTimestamp;

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

        private void RefreshSingleJobUI(PbJobModel job)
        {
            RemoveJobFromUI(job.JobId);

            // Build and Packed always contain the job
            lvBuild?.AddItem(job);
            packedListView2?.AddItem(job);

            // PickedUp only if shipped pallets exist  
            if (job.Pallets.Any(p => p.ShippedAt.HasValue))
            {
                pickedUpListView?.AddItem(job);
            }
        }
        public static List<ShipmentGroup> BuildShipments(List<PbJobModel> jobs)
{
    return jobs
        .SelectMany(j => j.Pallets)
        .Where(p => p.ShippedAt.HasValue)
        .GroupBy(p => p.ShippedAt.Value)
        .Select(g => new ShipmentGroup
        {
            ShippedAt = g.Key,
            Pallets = g.ToList()
        })
        .OrderByDescending(s => s.ShippedAt)
        .ToList();
}
        private void RemoveJobFromUI(int jobId)
        {
            lvBuild?.RemoveItem(jobId);
            packedListView2?.RemoveItem(jobId);
            pickedUpListView?.RemoveItem(jobId);
        }





        // -----------------------------
        // Form Events
        // -----------------------------
        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                await InitializeRqliteAsync();

                if (!await RqliteClient.IsDatabaseAvailableAsync())
                {
                    ShowDatabaseOfflineMessage();
                    return;
                }

                SyncNavigatorWithCheckedTab();
                await LoadJobsAsync();
                _lastJobsTimestamp = await RqliteClient.GetJobsLastUpdatedAsync();
                StartBackgroundPolling();

            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }
        private void PalletListView_PalletChanged(object sender, PbJobModel job)
        {
            RefreshAllViews();
        }

        private void PackedListView2_PackedDataChanged(object sender, PbJobModel job)
        {
            RefreshAllViews();
        }

        private void ShowDatabaseOfflineMessage()
        {
            MessageBox.Show(
                "Database service is not running.\n\nPlease start rqlite and restart the application.",
                "Database Offline",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        private void ShowDatabaseError(Exception ex)
        {
            MessageBox.Show(
                $"Database error:\n\n{ex.Message}",
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        private void ApplySearchFilter()
        {
            if (_pbJobs.Count == 0)
                return;

            var fromDate = dtPickUpFrom.Value.Date;
            var toDate = dtPickUpTo.Value.Date.AddDays(1).AddTicks(-1);

            var filtered = _pbJobs
                .Where(job =>
                {
                    // Use shipped date OR packed date depending on your business rule
                    var date = job.ShippedDate ?? job.PackDate;

                    if (!date.HasValue)
                        return false;

                    return date.Value >= fromDate && date.Value <= toDate;
                })
                .ToList();

            // Apply to the list below
            pickedUpListView.SetItems(filtered);
        }



        // -----------------------------
        // UI Styling / Wiring
        // -----------------------------
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
            var jobs = await RqliteClient.LoadJobsAsync();

            _pbJobs.Clear();
            _pbJobs.AddRange(jobs);

            _jobsById = jobs.ToDictionary(j => j.JobId);

            RefreshAllViews();
        }

        // -----------------------------
        // View Refresh
        // -----------------------------
        private void RefreshAllViews()
        {
            // =====================================================
            // 1️⃣ BUILD TAB  (Always show full original jobs)
            // =====================================================
            lvBuild?.SetItems(_pbJobs);


            // =====================================================
            // 2️⃣ PACKED & READY TAB  (ONLY NON-SHIPPED PALLETS)
            //    🔥 Deep clone to avoid reference contamination
            // =====================================================
            var activeJobs = _pbJobs
                .Select(job =>
                {
                    var activePallets = job.Pallets
                        .Where(p => p.State != PalletState.Shipped)
                        .Select(p => new Pallet
                        {
                            PalletId = p.PalletId,
                            PBJobId = p.PBJobId,
                            PalletNumber = p.PalletNumber,
                            PackedAt = p.PackedAt,
                            ShippedAt = p.ShippedAt,
                            TrayCount = p.TrayCount,
                            State = p.State,
                            WorkOrders = p.WorkOrders
                                .Select(w => new WorkOrder(w.WorkOrderCode, w.Quantity)
                                {
                                    Id = w.Id,
                                    PalletId = w.PalletId
                                })
                                .ToList()
                        })
                        .ToList();

                    if (!activePallets.Any())
                        return null;

                    return new PbJobModel
                    {
                        JobId = job.JobId,
                        JobName = job.JobName,
                        JobNumber = job.JobNumber,
                        IsTemp = job.IsTemp,
                        LastUpdated = job.LastUpdated,
                        Pallets = activePallets
                    };
                })
                .Where(j => j != null)
                .ToList();

            packedListView2?.SetItems(activeJobs);


            // =====================================================
            // 3️⃣ PICKUP TAB (ONLY SHIPPED PALLETS GROUPED BY TIME)
            // =====================================================
            var shipmentRows = new List<PbJobModel>();

            foreach (var job in _pbJobs)
            {
                var shippedGroups = job.Pallets
                    .Where(p => p.State == PalletState.Shipped && p.ShippedAt.HasValue)
                    .GroupBy(p => p.ShippedAt.Value);

                foreach (var group in shippedGroups)
                {
                    var shipmentClone = new PbJobModel
                    {
                        JobId = job.JobId,
                        JobName = job.JobName,
                        JobNumber = job.JobNumber,
                        IsTemp = job.IsTemp,
                        LastUpdated = job.LastUpdated,
                        ShippedDate = group.Key,
                        Pallets = group
                            .Select(p => new Pallet
                            {
                                PalletId = p.PalletId,
                                PBJobId = p.PBJobId,
                                PalletNumber = p.PalletNumber,
                                PackedAt = p.PackedAt,
                                ShippedAt = p.ShippedAt,
                                TrayCount = p.TrayCount,
                                State = p.State,
                                WorkOrders = p.WorkOrders
                                    .Select(w => new WorkOrder(w.WorkOrderCode, w.Quantity)
                                    {
                                        Id = w.Id,
                                        PalletId = w.PalletId
                                    })
                                    .ToList()
                            })
                            .ToList()
                    };

                    shipmentRows.Add(shipmentClone);
                }
            }

            shipmentRows = shipmentRows
                .OrderByDescending(s => s.ShippedDate)
                .ToList();

            pickedUpListView?.SetItems(shipmentRows);
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

            // Otherwise refresh item in place
           // lvBuild?.RefreshItem(existing);
           // packedListView2?.RefreshItem(existing);
           // pickedUpListView?.RefreshItem(existing);

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
        // Ship Pallets Bar
        // -----------------------------
        private async void btnShipPallets_Click(object sender, EventArgs e)
        {
            var selectedJobs = packedListView2.GetReadyJobs();

            if (!selectedJobs.Any())
            {
                MessageBox.Show("No jobs selected.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Ship {selectedJobs.Count} job(s)?",
                "Confirm Shipment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                foreach (var job in selectedJobs)
                {         
                    await RqliteClient.ShipJobsAsync(new[] { job.JobId });
                    await LoadJobsAsync();
                }
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        // -----------------------------
        // Settings
        // -----------------------------


        // -----------------------------
        // Actions
        // -----------------------------
        private async void btnAddPBJob_Click(object sender, EventArgs e)
        {
            using (var dlg = new CreatePBJobDialog())
            {
                if (dlg.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    int jobNumber = int.TryParse(dlg.JobNumber, out var num) ? num : 0;

                    // 🔴 PREVENT DUPLICATES
                    if (await RqliteClient.JobNumberExistsAsync(jobNumber))
                    {
                        MessageBox.Show(
                            $"Job number {jobNumber} already exists.",
                            "Duplicate Job",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    var job = new PbJobModel
                    {
                        JobName = dlg.JobName ?? string.Empty,
                        JobNumber = jobNumber,
                        IsTemp = true
                    };

                    await RqliteClient.InsertPbJobAsync(job);

                    await LoadJobsAsync();
                }
                catch (Exception ex)
                {
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
            packedListView2.SetAllSelected(      chkbxSelectAll.Checked);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var dlg = new SettingsDialog())
            {
                dlg.ShowDialog(this);
            }
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
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Packed_And_Ready;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        // -----------------------------
        // Fields
        // -----------------------------
        private readonly List<PbJobModel> _pbJobs = new List<PbJobModel>();
        

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
                try
                {
                    await RqliteClient.DeletePbJobAsync(job.JobId);
                    await LoadJobsAsync();
                }
                catch (Exception ex)
                {
                    ShowDatabaseError(ex);
                }
            };
        }

        // -----------------------------
        // ⭐ rqlite Initialization
        // -----------------------------
        private async Task InitializeRqliteAsync()
        {
            if (RqliteClient.httpClient == null)
            {
                RqliteClient.httpClient = new HttpClient();
                RqliteClient.DefaultEndPoint = "http://127.0.0.1:4001";
            }

            // Safe to call every run
            await RqliteClient.CreatePbSchemaAsync();
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
            _pbJobs.Clear();

            var jobs = await RqliteClient.LoadFullJobGraphAsync();

            _pbJobs.AddRange(jobs);
            RefreshAllViews();
        }

        // -----------------------------
        // View Refresh
        // -----------------------------
        private void RefreshAllViews()
        {
            lvBuild?.SetItems(_pbJobs);
            
            var shippedJobs = _pbJobs
        .Where(j => j.ShippedDate.HasValue)
        .ToList();
            var notShippedJobs = _pbJobs
        .Where(j => !j.ShippedDate.HasValue)
        .ToList();
            packedListView2?.SetItems(notShippedJobs);
            lvBuild?.SetItems(notShippedJobs);
            pickedUpListView?.SetItems(shippedJobs);
        }

        // -----------------------------
        // Window Buttons
        // -----------------------------
        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnMinimize_Click(object sender, EventArgs e) =>
            WindowState = FormWindowState.Minimized;

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            WindowState = (WindowState == FormWindowState.Maximized)
                ? FormWindowState.Normal
                : FormWindowState.Maximized;
        }

        // -----------------------------
        // Ship Pallets Bar
        // -----------------------------


        // -----------------------------
        // Settings
        // -----------------------------
        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var dlg = new SettingsDialog())
            {
                dlg.ShowDialog(this);
            }
        }

        // -----------------------------
        // Actions
        // -----------------------------
        private async void btnAddPBJob_Click_1(object sender, EventArgs e)
        {
            using (var dlg = new CreatePBJobDialog())
            {
                if (dlg.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    var job = new PbJobModel
                    {
                        JobName = dlg.JobName ?? string.Empty,
                        JobNumber = int.TryParse(dlg.JobNumber, out var num) ? num : 0,
                        IsReady = false,
                        PackDate = null
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
                    job.IsReady = true;
                    job.ShippedDate = DateTime.Now;

                    await RqliteClient.UpdateJobReadyAsync(job.JobId, true);
                    await RqliteClient.UpdateJobShippedDateAsync(
                        job.JobId,
                        job.ShippedDate.Value);
                }

                await LoadJobsAsync();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void chkbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            packedListView2.SetAllSelected(
      chkbxSelectAll.Checked
  );
        }

    
    }
}

using Jds2;
using Jds2.Interfaces;
using PitneyBowesCalculator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PitneyBowesCalculator.Packed_And_Ready.View_Button
{
    public partial class ViewButtonDialog : Form
    {
        /* -------------------------------------------------------------
         * FIELDS & DATA STORAGE
         * ------------------------------------------------------------- */

        private PbJobModel _job;
      
        private Panel _staleBanner;
        private bool _isStale = false;
        public bool DataChanged { get; private set; }

        /* -------------------------------------------------------------
         * WIN32 IMPORTS
         * ------------------------------------------------------------- */

        [DllImport("user32.dll")] private static extern bool ReleaseCapture();
        [DllImport("user32.dll")] private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

     

        /* -------------------------------------------------------------
         * CONSTRUCTOR
         * ------------------------------------------------------------- */

        public ViewButtonDialog(PbJobModel job, bool hideRemove = false, bool hidePrint = false, bool hideClose = true)
        {
            InitializeComponent();

            _job = job;

            btnRemovePallet.Visible = !hideRemove;
            btnPrintPallet.Visible = !hidePrint;

            LoadHeaderInfo();
            lvPallet.SetItems(_job);
            lvPallet.PalletClicked += OnPalletClicked;

            pnlHeader.MouseDown += pnlHeader_MouseDown;



            CSSDesign.AddPanelBorder(pnlDashboard, Color.Silver, 1);
            CSSDesign.AddPanelBorder(pnlDetails, Color.Silver, 1);
            CSSDesign.AddPanelBorder(pnlPalletNum, Color.Silver, 1);

            BuildStaleBanner();

        }



        /* -------------------------------------------------------------
         * HEADER
         * ------------------------------------------------------------- */

        private void LoadHeaderInfo()
        {
            txtPBJobName.Text = _job.JobName ?? string.Empty;
            txtPBJobNumber.Text = _job.JobNumber.ToString();
            bool isShipped = _job.ShippedDate.HasValue;

            if (isShipped)
            {
                // 🔁 Rename label
                lblPackDate.Text = "Shipped Date and Time";

                // Show shipped date + time
                txtPackDate.Text = _job.ShippedDate.Value.ToString("MM/dd/yyyy  hh:mm tt");
            }
            else
            {
                // Default behavior (Packed)
                lblPackDate.Text = "Packed Date";

                var hasPackedPallet =
                    _job.Pallets?.Any(p => p != null) == true;

                txtPackDate.Text = hasPackedPallet
                    ? _job.EffectivePackDate.ToString("MM/dd/yyyy")
                    : "--/--/----";
            }

        }

        /* -------------------------------------------------------------
         * DASHBOARD
         * ------------------------------------------------------------- */

        private void LoadDashboard(Pallet pallet)
        {
            if (pallet == null)
            {
                txtEnvelopeQty.Text = "0";
                txtScannedWO.Text = "0";
                txtTrayCount.Text = "0";
                txtPackedTime.Text = string.Empty;
                return;
            }

            bool isShipped = pallet.State == PalletState.Shipped;

            txtEnvelopeQty.Text = pallet.PalletEnvelopeQty.ToString("N0");
            txtScannedWO.Text = pallet.PalletScannedWO.ToString("N0");
            txtTrayCount.Text = pallet.TrayCount.ToString("N0");

            if (isShipped)
            {
                lblPackedTime.Text =
                    "Packed Date Time: " +
                    (pallet.PackedAt?.ToString("MM/dd/yyyy") ?? "--/--/----");

                txtPackedTime.Text = pallet.PackedAt.HasValue
                    ? pallet.PackedAt.Value.ToString("hh:mm tt")
                    : string.Empty;
            }
            else
            {
                txtPackedTime.Text = pallet.PackedAt.HasValue
                    ? pallet.PackedAt.Value.ToString("hh:mm tt")
                    : string.Empty;
            }
            
        }

        /* -------------------------------------------------------------
         * REMOVE PALLETS
         * ------------------------------------------------------------- */




        /* -------------------------------------------------------------
         * PALLET CLICK
         * ------------------------------------------------------------- */

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            lvPallet.SelectFirstPallet();
        }
        private void OnPalletClicked(int index)
        {
            if (index < 0 || index >= _job.Pallets.Count)
                return;

            var pallet = _job.Pallets[index];
            LoadDashboard(pallet);
            lvPalletDetails.SetItems(pallet.WorkOrders);
        }

        /* -------------------------------------------------------------
         * FORM BEHAVIOR
         * ------------------------------------------------------------- */

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }




        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnRemovePallet_Click_1(object sender, EventArgs e)
        {
            try
            {
                // 🔥 ALWAYS refresh first (avoid stale indices)
                await PBCMain.Instance.RefreshSingleJobAsync(_job.JobId);

                var selectedIndices = lvPallet.GetSelectedIndices();

                if (selectedIndices == null || selectedIndices.Count == 0)
                {
                    MessageDialogBox.ShowDialog("Error", "Please select at least one pallet.", MessageBoxButtons.OK, MessageType.Info);
                    return;
                }

                // 🔥 Rebuild selection from fresh model
                var selectedPallets = selectedIndices
                    .Where(i => i >= 0 && i < _job.Pallets.Count)
                    .Select(i => _job.Pallets[i])
                    .ToList();

                if (!selectedPallets.Any())
                {
                    await PBCMain.Instance.RefreshSingleJobAsync(_job.JobId);
                    return;
                }

                // 🔒 Validate states again
                if (!selectedPallets.All(p => p.State == PalletState.Ready))
                {
                    MessageDialogBox.ShowDialog(
                        "Invalid Selection",
                        "Only packed pallets (Ready) can be removed.",
                        MessageBoxButtons.OK,
                        MessageType.Warning
                    );
                    return;
                }

                var palletIds = selectedPallets
                    .Select(p => p.PalletId)
                    .Where(id => id > 0)
                    .Distinct()
                    .ToList();

                if (!palletIds.Any())
                {
                    MessageDialogBox.ShowDialog("", "Invalid pallet selection.", MessageBoxButtons.OK, MessageType.Error);
                    return;
                }

                // 🔥 get latest active pallet (still safe to fetch now)
                var activePalletId = await RqliteClient.GetActivePalletIdAsync(_job.JobId);
                bool hasActivePallet = activePalletId != null;

                using (var dlg = new RemovePallets(hasActivePallet))
                {
                    if (dlg.ShowDialog(this) != DialogResult.OK)
                        return;

                    // 🔥 CRITICAL: re-check AFTER dialog
                    await PBCMain.Instance.RefreshSingleJobAsync(_job.JobId);
                    // AFTER refresh
                    activePalletId = await RqliteClient.GetActivePalletIdAsync(_job.JobId);
                    hasActivePallet = activePalletId != null;

                    // 🔥 Rebuild again (DO NOT trust old selection)
                    selectedPallets = selectedIndices
                        .Where(i => i >= 0 && i < _job.Pallets.Count)
                        .Select(i => _job.Pallets[i])
                        .ToList();

                    palletIds = selectedPallets
                        .Select(p => p.PalletId)
                        .Where(id => id > 0)
                        .Distinct()
                        .ToList();

                    if (!palletIds.Any())
                    {
                       
                        return;
                    }

                    switch (dlg.Action)
                    {
                        case RemovePallets.RemoveAction.Merge:
                            if (activePalletId == null)
                            {
                                MessageDialogBox.ShowDialog("", "No active pallet available.", MessageBoxButtons.OK, MessageType.Error);
                                return;
                            }

                            await RqliteClient.MergePalletsIntoAsync(palletIds, activePalletId.Value);
                            break;

                        case RemovePallets.RemoveAction.Delete:
                            await RqliteClient.DeletePalletsAsync(palletIds);
                            break;

                        case RemovePallets.RemoveAction.UndoPack:

                            if (!hasActivePallet && palletIds.Count > 1)
                            {
                                int targetPalletId = palletIds.OrderBy(id => id).First();

                                await RqliteClient.UndoPackedPalletAsync(
                                    new List<int> { targetPalletId },
                                    _job.JobId);

                                await RqliteClient.MergePalletsIntoAsync(
                                    palletIds.Where(id => id != targetPalletId),
                                    targetPalletId);
                            }
                            else
                            {
                                await RqliteClient.UndoPackedPalletAsync(
                                    palletIds,
                                    _job.JobId);
                            }
                            break;

                        default:
                            return;
                    }
                }

             
                var fresh = await RqliteClient.LoadSingleJobGraphAsync(_job.JobId);
                if (fresh?.LastUpdatedRaw != null)
                    PBCMain.Instance.MarkPendingUpdate(_job.JobId, fresh.LastUpdatedRaw);

                DataChanged = true;
                Close();
            }
            catch (Exception ex)
            {
                Utils.WriteUnexpectedError($"RemovePallet failed | JobId={_job?.JobId}");
                Utils.WriteExceptionError(ex);
                MessageDialogBox.ShowDialog("", "Error processing pallets:\n\n" + ex.Message, MessageBoxButtons.OK, MessageType.Error);
            }
        }

        private async void btnPrintPallet_Click_1(object sender, EventArgs e)
        {
            btnPrintPallet.Enabled = false;

            Utils.showStatusAndSpinner(lbStatus, pbSpinner, "Attempting to print...");

            try
            {
                var selectedIndices = lvPallet.GetSelectedIndices();

                // ✅ Validate selection early
                if (selectedIndices == null || selectedIndices.Count == 0)
                {
                    Utils.hideStatusAndSpinner(lbStatus, pbSpinner, "");

                    MessageDialogBox.ShowDialog(
                        "No Selection",
                        "Select pallet(s) to print.",
                        MessageBoxButtons.OK,
                        MessageType.Info
                    );
                    return;
                }

                var selectedPallets = selectedIndices
                    .Select(i => _job.Pallets[i])
                    .ToList();

                // ✅ Optional: ensure pallets are packed
                var invalid = selectedPallets
                    .Where(p => p.PackedAt == null)
                    .ToList();

                if (invalid.Any())
                {
                    Utils.hideStatusAndSpinner(lbStatus, pbSpinner, "");

                    MessageDialogBox.ShowDialog(
                        "Invalid Selection",
                        "Only packed pallets can be printed.",
                        MessageBoxButtons.OK,
                        MessageType.Warning
                    );
                    return;
                }

                // ✅ Run print safely
                await Task.Run(() =>
                {
                    PrintEngine.Print(e =>
                        PrintLayouts.DrawPallets(e, _job, selectedPallets)
                    );
                });

                // ✅ Only reached if NO exception
                Utils.hideStatusAndSpinner(lbStatus, pbSpinner, "Printed successfully!");
            }
            catch (Exception ex)
            {
                Utils.WriteUnexpectedError($"PrintPallet failed | JobId={_job?.JobId}");  // ✅ add
                Utils.WriteExceptionError(ex);
                Utils.errorStatusAndSpinner(lbStatus, pbSpinner, "Print failed!");

                MessageDialogBox.ShowDialog(
                    "Printing Error",
                    "Failed to print pallet.\n\n" + ex.Message,
                    MessageBoxButtons.OK,
                    MessageType.Warning
                );
            }
            finally
            {
                btnPrintPallet.Enabled = true;
            }
        }

        private void BuildStaleBanner()
        {
            _staleBanner = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(230, 126, 34),
                Visible = false
            };

            var btnRefresh = new Button
            {
                Text = "Refresh",
                Dock = DockStyle.Right,
                Width = 80,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(211, 84, 0),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += async (s, e) => await RefreshFromDbAsync();

            var lbl = new Label
            {
                Text = "⚠  Data has been updated by another workstation.",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            // ✅ Button first so it docks right, label fills remaining
            _staleBanner.Controls.Add(btnRefresh);
            _staleBanner.Controls.Add(lbl);

            this.Controls.Add(_staleBanner);
            _staleBanner.BringToFront();
        }

        public void NotifyStaleData()
        {
            if (this.IsDisposed || !this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
            {
                try { this.Invoke((Action)NotifyStaleData); }
                catch (ObjectDisposedException) { }  // ✅ guard the race
                return;
            }

            _isStale = true;
            _staleBanner.Visible = true;
            btnRemovePallet.Enabled = false;
        }

        private async Task RefreshFromDbAsync()
        {
            try
            {
                var freshJob = await RqliteClient.LoadSingleJobGraphAsync(_job.JobId);

                if (freshJob == null)
                {
                    MessageDialogBox.ShowDialog(
                        "Job Deleted",
                        "This job has been deleted by another workstation.",
                        MessageBoxButtons.OK,
                        MessageType.Warning
                    );
                    this.Close();
                    return;
                }

                // ✅ Apply same filter as packedListView2 in PBCMain.RefreshSingleJobUI
                freshJob.Pallets = freshJob.Pallets
                    .Where(p => p.State == PalletState.Ready ||
                                p.State == PalletState.Packed_NotReady)
                    .ToList();

                if (!freshJob.Pallets.Any())
                {
                    MessageDialogBox.ShowDialog(
                        "No Pallets",
                        "All pallets have been removed or shipped by another workstation.",
                        MessageBoxButtons.OK,
                        MessageType.Warning
                    );
                    this.Close();
                    return;
                }

                // ✅ Update local model with filtered pallets
                _job = freshJob;

                // ✅ Reload all UI from fresh filtered data
                LoadHeaderInfo();
                lvPallet.SetItems(_job);
                lvPallet.SelectFirstPallet();

                // ✅ Clear stale state
                _isStale = false;
                _staleBanner.Visible = false;
                btnRemovePallet.Enabled = true;
            }
            catch (Exception ex)
            {
                Utils.WriteUnexpectedError($"RefreshFromDb failed | JobId={_job?.JobId}");  // ✅ add
                Utils.WriteExceptionError(ex);
                MessageDialogBox.ShowDialog("", "Refresh failed: " + ex.Message, MessageBoxButtons.OK, MessageType.Warning);
            }
        }
    }
}
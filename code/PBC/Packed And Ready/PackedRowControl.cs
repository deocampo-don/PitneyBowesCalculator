using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Packed_And_Ready.View_Button;

namespace WindowsFormsApp1.Packed_And_Ready
{
    public partial class PackedRowControl : UserControl
    {
        /* -------------------------------------------------------------
         * FIELDS
         * ------------------------------------------------------------- */

        public event EventHandler SelectionChanged;
        private bool _updatingUI;
        private PbJobModel _modelpbjob;
        public event EventHandler DataChanged;
        public PbJobModel BoundJob { get; private set; }
        public event EventHandler<PbJobModel> PackedDataChanged;

        public event EventHandler ViewClicked;
        private bool _isBinding;

        public event EventHandler ViewDialogClosed;


        /* -------------------------------------------------------------
         * CONSTRUCTOR
         * ------------------------------------------------------------- */

        public PackedRowControl()
        {
            InitializeComponent();
            
            chkbxStatus.Checked = true;
            
        }

        public void Bind(PbJobModel job)
        {
            if (job == null)
                return;

            BoundJob = job;
            _modelpbjob = job;

            txtPBJobName.Text = job.JobName ?? string.Empty;
            txtPBJobNum.Text = job.JobNumber.ToString();

            txtEnvelopeQty.Text = job.TotalEnvelopeOfJob.ToString();
            txtPallets.Text = (job.Pallets?.Count ?? 0).ToString();
            txtTrays.Text = job.TotalTraysOfJob.ToString();

            // ===== PACK DATE =====
            var hasPackedPallet = job.Pallets?.Any(p => p.PackedAt.HasValue) == true;

            txtPackDate.Text = hasPackedPallet
                ? job.EffectivePackDate.ToString("MM/dd/yyyy")
                : "--/--/----";

            // ===== READY CHECK =====
            var activePallets = job.Pallets?
                .Where(p =>
                    p.State != PalletState.NotReady &&
                    p.State != PalletState.Shipped)
                .ToList();

            bool isReady =
                activePallets != null &&
                activePallets.Count > 0 &&
                activePallets.All(p =>
                    p.State == PalletState.Ready &&
                    p.PackedAt.HasValue);

            _isBinding = true;
            chkbxStatus.Checked = isReady;
            _isBinding = false;
            if (isReady)
            {
                txtStatus.Text = "Ready to Ship";
                txtStatus.StateCommon.ShortText.Color1 =
                    ColorTranslator.FromHtml("#34C759"); // green
            }
            else
            {
                txtStatus.Text = "Not Ready";
                txtStatus.StateCommon.ShortText.Color1 =
                    ColorTranslator.FromHtml("#FF383C"); // red
            }
        }

        /* -------------------------------------------------------------
         * UI EVENTS
         * ------------------------------------------------------------- */


        public void SetReadyToShip(bool isReady)
        {
            chkbxStatus.Checked = isReady;
        }


        /// <summary>
        /// Toggles status text and color based on checkbox.
        /// </summary>


        private void btnView_Click_1(object sender, EventArgs e)
        {
            ViewClicked?.Invoke(this, EventArgs.Empty);

            Form parentForm = this.FindForm();

            PBCMain.PausePolling = true;   // ⭐ pause polling

            using (var dlg = new ViewButtonDialog(_modelpbjob))
            {
                dlg.ShowDialog(parentForm);

                if (dlg.DataChanged)
                {
                    ViewDialogClosed?.Invoke(this, EventArgs.Empty);
                }
            }

            PBCMain.PausePolling = false;  // ⭐ resume polling
        }

        private async void chkbxStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (_isBinding)
                return;

            if (_modelpbjob == null)
                return;

            try
            {
                if (chkbxStatus.Checked)
                {
                    // Update memory immediately
                    foreach (var p in _modelpbjob.Pallets
                        .Where(p => p.State == PalletState.Packed_NotReady))
                    {
                        p.State = PalletState.Ready;
                    }

                    txtStatus.Text = "Ready to Ship";
                    txtStatus.StateCommon.ShortText.Color1 =
                        ColorTranslator.FromHtml("#34C759");

                    PackedDataChanged?.Invoke(this, _modelpbjob);

                    // Update database
                    await RqliteClient.TogglePalletReadyAsync(
                        _modelpbjob.JobId,
                        PalletState.Packed_NotReady,
                        PalletState.Ready);
                }
                else
                {
                    foreach (var p in _modelpbjob.Pallets
                        .Where(p => p.State == PalletState.Ready))
                    {
                        p.State = PalletState.Packed_NotReady;
                    }

                    txtStatus.Text = "Not Ready";
                    txtStatus.StateCommon.ShortText.Color1 =
                        ColorTranslator.FromHtml("#FF383C");

                    PackedDataChanged?.Invoke(this, _modelpbjob);

                    await RqliteClient.TogglePalletReadyAsync(
                        _modelpbjob.JobId,
                        PalletState.Ready,
                        PalletState.Packed_NotReady);
                }
            }
            catch (Exception ex)
            {

              /*MessageBox.Show(
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
              */

                MessageDialogBox.ShowDialog(
                    "Database Error",
                    ex.Message,
                    MessageBoxButtons.OK,
                    MessageType.Error
                );
            }
        }
        public bool IsSelected()
        {
            return chkbxStatus.Checked && !_modelpbjob.ShippedDate.HasValue;
        }
        public void SetChecked(bool value)
        {
            if (_modelpbjob.ShippedDate.HasValue)
                return;

            chkbxStatus.Checked = value;
        }
        public bool IsReady()
        {
            return chkbxStatus.Checked;
        }

        public PbJobModel GetModel()
        {
            return _modelpbjob;
        }
    }
}

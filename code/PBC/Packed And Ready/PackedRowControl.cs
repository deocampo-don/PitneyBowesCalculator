using Microsoft.IdentityModel.Tokens;
using PitneyBowesCalculator;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PitneyBowesCalculator.Packed_And_Ready.View_Button;

namespace PitneyBowesCalculator.Packed_And_Ready
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
        private bool _suppressEvents = false;
        private static int _cbSelected = 0;


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


        private void btnView_Click_1(object sender, EventArgs e)
        {
            ViewClicked?.Invoke(this, EventArgs.Empty);

            Form parentForm = this.FindForm();

            

            using (var dlg = new ViewButtonDialog(_modelpbjob))
            {
                dlg.ShowDialog(parentForm);

                if (dlg.DataChanged)
                {
                    ViewDialogClosed?.Invoke(this, EventArgs.Empty);
                }
            }

           
        }

        private async void chkbxStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (_isBinding || _modelpbjob == null)
                return;

            _suppressEvents = true;
            this.Enabled = false;

            try
            {
                bool isReady = chkbxStatus.Checked;

                var fromState = isReady
                    ? PalletState.Packed_NotReady
                    : PalletState.Ready;

                var toState = isReady
                    ? PalletState.Ready
                    : PalletState.Packed_NotReady;

                foreach (var p in _modelpbjob.Pallets)
                {
                    if (p.State == fromState)
                        p.State = toState;
                }

                txtStatus.Text = isReady ? "Ready to Ship" : "Not Ready";
                txtStatus.StateCommon.ShortText.Color1 =
                    ColorTranslator.FromHtml(isReady ? "#34C759" : "#FF383C");

                var ts = await RqliteClient.TogglePalletReadyAsync(
                    _modelpbjob.JobId,
                    fromState,
                    toState
                );

                if (!string.IsNullOrEmpty(ts))
                {
                    PBCMain.Instance.MarkPendingUpdate(_modelpbjob.JobId, ts);
                }
            }
            catch (Exception ex)
            {
                Utils.WriteExceptionError(ex);
            }
            finally
            {
                this.Enabled = true;
                _suppressEvents = false;
            }
        }

        public void SetChecked(bool value)
        {
            if (_modelpbjob.ShippedDate.HasValue)
                return;

            chkbxStatus.Checked = value;
        }

    }
}

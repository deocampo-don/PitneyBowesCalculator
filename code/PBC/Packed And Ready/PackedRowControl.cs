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


        public PbJobModel BoundJob { get; private set; }


        public event EventHandler ViewClicked;

        
        public event EventHandler ViewDialogClosed;


        /* -------------------------------------------------------------
         * CONSTRUCTOR
         * ------------------------------------------------------------- */

        public PackedRowControl()
        {
            InitializeComponent();
            
            chkbxStatus.Checked = true;
            // Style enhancements for cleaner UI

        }


        /* -------------------------------------------------------------
         * DATA BINDING
         * ------------------------------------------------------------- */

        /// <summary>
        /// Binds a PB job model to the row and updates all UI fields.
        /// </summary>
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


            // ===== STATE =====
            bool isShipped = job.Pallets?.Any() == true &&
                             job.Pallets.All(p => p.State == PalletState.Shipped);

            bool isReady =
                      job.Pallets != null &&
                      job.Pallets.Count > 0 &&
                      job.Pallets.All(p => p.State == PalletState.Packed);

            if (isShipped)
            {
                chkbxStatus.Checked = true;
                chkbxStatus.Enabled = false;

                txtStatus.Text = "Shipped";
                txtStatus.StateCommon.ShortText.Color1 =
                    ColorTranslator.FromHtml("#007AFF"); // blue
            }
            else
            {
                chkbxStatus.Enabled = true;
                chkbxStatus.Checked = isReady;

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
        }



        /* -------------------------------------------------------------
         * UI EVENTS
         * ------------------------------------------------------------- */


        public void SetReadyToShip(bool isReady)
        {
            chkbxStatus.Checked = isReady;
        }


        private void btnView_Click(object sender, EventArgs e)
        {
            // Optional: notify listeners that View was clicked
            ViewClicked?.Invoke(this, EventArgs.Empty);

            Form parentForm = this.FindForm();
            var clonedJob = ModelCloner.CloneJob(_modelpbjob);

            using (var dlg = new ViewButtonDialog(clonedJob))
            {
                dlg.ShowDialog(parentForm);

                // ✅ Notify parent ONLY if data changed
                if (dlg.DataChanged)
                {
                    ViewDialogClosed?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        /// <summary>
        /// Toggles status text and color based on checkbox.
        /// </summary>
    

        private void btnView_Click_1(object sender, EventArgs e)
        {
            // Optional: notify listeners that View was clicked
            ViewClicked?.Invoke(this, EventArgs.Empty);

            Form parentForm = this.FindForm();
            using (var dlg = new ViewButtonDialog(_modelpbjob))
            {
                dlg.ShowDialog(parentForm);

                // ✅ Notify parent ONLY if data changed
                if (dlg.DataChanged)
                {
                    ViewDialogClosed?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        private void chkbxStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (_modelpbjob == null)
                return;

            // Already shipped → do nothing
            if (_modelpbjob.ShippedDate.HasValue)
                return;

            // Only validate when CHECKING
            if (chkbxStatus.Checked)
            {
                bool allPacked =
                    _modelpbjob.Pallets.Any() &&
                    _modelpbjob.Pallets.All(p => p.State == PalletState.Packed);

                if (!allPacked)
                {
                    // Prevent infinite recursion
                    chkbxStatus.CheckedChanged -= chkbxStatus_CheckedChanged;
                    chkbxStatus.Checked = false;
                    chkbxStatus.CheckedChanged += chkbxStatus_CheckedChanged;

                    MessageBox.Show(
                        "There is no pallet or some pallets are not yet packed.",
                        "Cannot Ship",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                txtStatus.Text = "Ready to Ship";
                txtStatus.StateCommon.ShortText.Color1 =
                    ColorTranslator.FromHtml("#34C759");
            }
            else
            {
                txtStatus.Text = "Not Ready";
                txtStatus.StateCommon.ShortText.Color1 =
                    ColorTranslator.FromHtml("#FF383C");
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

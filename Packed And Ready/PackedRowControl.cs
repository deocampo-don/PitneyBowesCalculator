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


      
        private PbJobModel _modelpbjob;

      
        private Pallet _modelpallet;

        public event EventHandler ViewClicked;

        
        public event EventHandler ViewDialogClosed;


        /* -------------------------------------------------------------
         * CONSTRUCTOR
         * ------------------------------------------------------------- */

        public PackedRowControl()
        {
            InitializeComponent();

            // Style enhancements for cleaner UI
            CSSDesign.MakeRounded(btnView, 10);
            CSSDesign.MakePanelRounded(pnlDashboard, 12, Color.Gray, 2);
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

            _modelpbjob = job;

            // Job identity
            txtPBJobName.Text = job.JobName ?? string.Empty;
            txtPBJobNum.Text = job.JobNumber.ToString();

            // Job totals (computed in the model)
            txtEnvelopeQty.Text = job.TotalEnvelopeOfJob.ToString();
            txtPallets.Text = (job.Pallets?.Count ?? 0).ToString();
            txtTrays.Text = job.TotalTraysOfJob.ToString();

            // Job-level date
            var hasPackedPallet = job.Pallets?.Any(p => p != null /* optionally: && p.WorkOrders?.Any() == true */) == true;

            txtPackDate.Text =
                hasPackedPallet
                    ? job.EffectivePackDate.ToString("MM/dd/yyyy")
                    : "--/--/----";


        }



        /* -------------------------------------------------------------
         * UI EVENTS
         * ------------------------------------------------------------- */

        /// <summary>
        /// Triggers callback and opens the View dialog.
        /// </summary>
        /// 


        public void SetReadyToShip(bool isReady)
        {
            chkbxStatus.Checked = isReady;
        }


        private void btnView_Click(object sender, EventArgs e)
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


        /// <summary>
        /// Toggles status text and color based on checkbox.
        /// </summary>
        private void chkbxStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxStatus.Checked)
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
}

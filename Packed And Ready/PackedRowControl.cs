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

        /// <summary>
        /// Model containing PB Job information for this row.
        /// </summary>
        private PbJobModel _modelpbjob;

        /// <summary>
        /// Optional pallet data (reserved for future features).
        /// </summary>
        private Pallet _modelpallet;

        /// <summary>
        /// Event fired when user clicks the VIEW button.
        /// Allows parent container to intercept or override behavior.
        /// </summary>
        public event EventHandler ViewClicked;


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
        public void Bind(PbJobModel job )
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
            txtPackDate.Text = job.PackDate.ToShortDateString();

        }


        /* -------------------------------------------------------------
         * UI EVENTS
         * ------------------------------------------------------------- */

        /// <summary>
        /// Triggers callback and opens the View dialog.
        /// </summary>
        private void btnView_Click(object sender, EventArgs e)
        {
            // Notify parent listeners (optional override)
            ViewClicked?.Invoke(this, EventArgs.Empty);

            // Default behavior: Open View dialog
            Form parentForm = this.FindForm();
            using (ViewButtonDialog dlg = new ViewButtonDialog(_modelpbjob))
            {
                dlg.ShowDialog(parentForm);
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
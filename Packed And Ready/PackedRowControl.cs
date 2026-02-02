
using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Packed_And_Ready.View_Button;

namespace WindowsFormsApp1.Packed_And_Ready
{
    public partial class PackedRowControl : UserControl
    {
        private PickListModel _model;

        // Expose an event so parent can react to View button
        public event EventHandler ViewClicked;

        public PackedRowControl()
        {
            InitializeComponent();
         
            
            

            CSSDesign.MakeRounded(btnView, 10);
            CSSDesign.MakePanelRounded(pnlDashboard, 12, Color.Gray, 2);
            

          
            
        }

        // Optional bind from a model if you have one
        public void Bind(PickListModel model)
        {
            _model = model;

            txtPBJobName.Text = model.JobName;       // ensure your model has these properties
            txtPBJobNum.Text = model.JobNumber.ToString();
            txtEnvelopeQty.Text = model.EnvelopeQty.ToString();
            txtTrays.Text = model.Trays.ToString();
            txtPallets.Text = model.Pallets.ToString();
            txtPackDate.Text = model.ShipDateTime.ToShortDateString();

        }

      

        // === UI events ===

        private void btnView_Click(object sender, EventArgs e)
        {
            // Raise an event in case parent wants to intercept
            ViewClicked?.Invoke(this, EventArgs.Empty);

            // Keep your dialog behavior
            Form parentForm = this.FindForm();
            using (ViewButtonDialog dlg = new ViewButtonDialog())
            {
                dlg.ShowDialog(parentForm);
            }
        }

        private void chkbxStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxStatus.Checked)
            {
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
    }
}

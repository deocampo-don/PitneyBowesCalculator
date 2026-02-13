using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Packed_And_Ready.View_Button;

namespace WindowsFormsApp1.Picked_Up
{
    public partial class PickedUpRowControl : UserControl
    {

        private PbJobModel _model;
        public event EventHandler ViewClicked;
        public event EventHandler ViewDialogClosed;
        private PbJobModel _modelpbjob;

        public PbJobModel BoundJob { get; private set; }
        public PickedUpRowControl()
        {
            InitializeComponent();

  
        }

        public void Bind(PbJobModel model)
        {
            _model = model;
            _modelpbjob = model;
            BoundJob = model;

            lblPBNameCode.Text = model.JobNumber + " " + model.JobName;

            lblQty.Text = model.TotalEnvelopeOfJob.ToString();
            lblTrays.Text = model.TotalTraysOfJob.ToString();
            lblPallets.Text = model.Pallets.Count.ToString();
            lblShipTime.Text = model.ShippedDate.ToString();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
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
    }
}

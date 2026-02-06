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

namespace WindowsFormsApp1.Picked_Up
{
    public partial class PickedUpRowControl : UserControl
    {

        private PbJobModel _model;
        public PickedUpRowControl()
        {
            InitializeComponent();

  
        }

        public void Bind(PbJobModel model)
        {
            _model = model;

            lblPBNameCode.Text = model.JobNumber + " " + model.JobName;

            lblQty.Text = model.TotalEnvelopeOfJob.ToString();
            lblTrays.Text = model.TotalTraysOfJob.ToString();
            lblPallets.Text = model.Pallets.Count.ToString();
            lblShipTime.Text = model.PackDate.ToString();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

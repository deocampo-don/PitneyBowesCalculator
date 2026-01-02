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

        private PickListModel _model;
        public PickedUpRowControl()
        {
            InitializeComponent();

  
        }

        public void Bind(PickListModel model)
        {
            _model = model;

            lblPBNameCode.Text = model.JobNumber + " " + model.JobName;

            lblQty.Text = model.EnvelopeQty.ToString("N0");
            lblTrays.Text = model.Trays.ToString("N0");
            lblPallets.Text = model.Pallets.ToString("No");
            lblShipTime.Text = model.ShipDateTime.ToString();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

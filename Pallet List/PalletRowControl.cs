using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.DIalogs;

namespace WindowsFormsApp1
{
    public partial class PalletRowControl : UserControl
    {

        private PbJobModel _model;
        public PalletRowControl()
        {
            InitializeComponent();

        }

        private void PanelTableLayout_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RoundedGroupBox_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddPallet_Click(object sender, EventArgs e)
        {
            using (var dlg = new AddToPalletDialog())
            {
                dlg.ShowDialog(this); // modal, centered to parent
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            var items = new List<WorkOrderItem>
    {
        new WorkOrderItem { Code = "CXXX26010101PER0001", Quantity = 150 },
        new WorkOrderItem { Code = "CXXX26010101PER0002", Quantity = 1500 },
        new WorkOrderItem { Code = "CXXX26020103PER0001", Quantity = 1500 },
    };

            using (var dlg = new ViewWOListDialog())
            {
                dlg.SetItems(items);

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    var selected = items.Where(x => x.IsSelected).ToList();
                    // Use selected items
                }
            }
        }

        private void PanelTableLayout_Paint_1(object sender, PaintEventArgs e)
        {

        }

        public void Bind(PbJobModel model)
        {
            _model = model;

            lblPbJobName.Text = model.JobName;
            lblAxRef.Text = model.JobNumber.ToString(); 

            lblEnvelopeQty.Text = model.EnvelopeQty.ToString("N0");
            lblScannedWOs.Text = model.ScannedWorkOrders.ToString("N0");
        }

     

        private void btnPackPallet_Click(object sender, EventArgs e)
        {
            using (var dlg = new PackPalletDIalog())
            {
                dlg.ShowDialog(this); // modal, centered to parent
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class WorkOrderRowControl : UserControl
    {

        private WorkOrderItem _item;
        public WorkOrderRowControl()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void Bind(WorkOrderItem item)
        {
            _item = item;
            lblWOname.Text = item.Code;
            lblWOqty.Text = item.Quantity.ToString();
            cbWO.Checked = item.IsSelected;
        }

        private void cbWO_CheckedChanged(object sender, EventArgs e)
        {
            if (_item != null)
                _item.IsSelected = cbWO.Checked;
        }

        public bool ShowDivider
        {
            set => pnlDivider.Visible = value;
        }

        private void WorkOrderRowControl_Load(object sender, EventArgs e)
        {

        }
    }
}

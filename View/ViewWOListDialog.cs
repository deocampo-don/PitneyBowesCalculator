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
    public partial class ViewWOListDialog : Form
    {

        private List<WorkOrderItem> _items;

        public ViewWOListDialog()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        private void ViewWOListDialog_Load(object sender, EventArgs e)
        {

        }

        public void SetItems(IEnumerable<WorkOrderItem> items)
        {
            _items = items.ToList(); // store reference

            woFlowRows.SuspendLayout();
            woFlowRows.Controls.Clear();

            for (int i = 0; i < _items.Count; i++)
            {
                var row = new WorkOrderRowControl();
                row.Bind(_items[i]);
                row.ShowDivider = i < _items.Count - 1;
                woFlowRows.Controls.Add(row);
            }

            woFlowRows.ResumeLayout();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

      
        private void btnCancelWo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClearWo_Click(object sender, EventArgs e)
        {
            _items.RemoveAll(item => item.IsSelected);

            // Rebuild the list UI
            SetItems(_items);
        }
    }
}

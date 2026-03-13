using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class ViewWOListDialog : Form
    {
        private List<WorkOrder> _items;
        public List<WorkOrder> DeletedItems { get; private set; }
        private string jobname;
        private string jobn;

        public ViewWOListDialog()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this);
            ShadowHelper.ApplyShadow(this);

        }

        public void SetItems(string jobName, string jobNum, IEnumerable<WorkOrder> items)
        {

            jobname = jobName;
            jobn = jobNum;
            lbPbName.Text = jobname;
            lbPbNum.Text = jobn;

            _items = items.ToList();

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


        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelWo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnClearWo_Click(object sender, EventArgs e)
        {
            var selectedRows = woFlowRows.Controls
        .OfType<WorkOrderRowControl>()
        .Where(r => r.IsSelected)
        .ToList();

            if (!selectedRows.Any())
            {
                MessageBox.Show("No work orders selected.");
                return;
            }

            var confirm = MessageBox.Show(
                "Delete selected work orders?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            DeletedItems = selectedRows
                .Select(r => r.BoundItem)
                .ToList();

            try
            {
                await RqliteClient.DeleteWorkOrdersAsync(
                    DeletedItems.Select(w => w.Id)
                );

                // Remove from dialog list
                foreach (var wo in DeletedItems)
                    _items.Remove(wo);

                SetItems(jobname,jobn,_items);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete failed: " + ex.Message);
            }
        }
    }
}

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


        private void btnCancelWo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private async void btnClearWo_Click(object sender, EventArgs e)
        //{
        //    var selectedRows = woFlowRows.Controls
        //.OfType<WorkOrderRowControl>()
        //.Where(r => r.IsSelected)
        //.ToList();

        //    if (!selectedRows.Any())
        //    {

        //        MessageDialogBox.ShowDialog("", "No work orders selected.", MessageBoxButtons.OK, MessageType.Info);
        //        return;
        //    }

        //    var confirm = MessageDialogBox.ShowDialog(
        //        "Confirm Delete",
        //        "Delete selected work orders?",
        //        MessageBoxButtons.YesNo,
        //        MessageType.Warning
        //    );

        //    if (confirm != DialogResult.Yes)
        //        return;

        //    DeletedItems = selectedRows
        //        .Select(r => r.BoundItem)
        //        .ToList();

        //    try
        //    {
        //        await RqliteClient.DeleteWorkOrdersAsync(
        //            DeletedItems.Select(w => w.Id)
        //        );

        //        // Remove from dialog list
        //        foreach (var wo in DeletedItems)
        //            _items.Remove(wo);
        //        if (DeletedItems.Count == 0){
        //            MessageDialogBox.ShowDialog("", "Pallet is now empty. Deleting pallet..", MessageBoxButtons.OK, MessageType.Info);
        //        }
        //        SetItems(jobname,jobn,_items);
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.WriteExceptionError(ex);             
        //        MessageDialogBox.ShowDialog("", "Delete failed: " + ex.Message, MessageBoxButtons.OK, MessageType.Info);
        //    }
        //}
        private async void btnClearWo_Click(object sender, EventArgs e)
        {
            var selectedRows = woFlowRows.Controls
                .OfType<WorkOrderRowControl>()
                .Where(r => r.IsSelected)
                .ToList();

            if (!selectedRows.Any())
            {
                MessageDialogBox.ShowDialog("", "No work orders selected.", MessageBoxButtons.OK, MessageType.Info);
                return;
            }

            var confirm = MessageDialogBox.ShowDialog(
                "Confirm Delete",
                "Delete selected work orders?",
                MessageBoxButtons.YesNo,
                MessageType.Warning
            );

            if (confirm != DialogResult.Yes)
                return;

            DeletedItems = selectedRows
                .Select(r => r.BoundItem)
                .ToList();

            try
            {
                var ids = DeletedItems.Select(w => w.Id).ToList();
                var palletId = DeletedItems.First().PalletId;

                // STEP 1: Delete in DB
                await RqliteClient.DeleteWorkOrdersAsync(ids);

                // STEP 2: Update UI list
                foreach (var wo in DeletedItems)
                    _items.Remove(wo);

                // ⭐ STEP 3: If empty → delete pallet
                if (!_items.Any())
                {
                    await RqliteClient.DeletePalletsAsync(new[] { palletId });

                    MessageDialogBox.ShowDialog("",
                        "No work orders left. Pallet will be removed.",
                        MessageBoxButtons.OK,
                        MessageType.Info);
                    this.Close();
                }

                SetItems(jobname, jobn, _items);
            }
            catch (Exception ex)
            {
                Utils.WriteExceptionError(ex);
                MessageDialogBox.ShowDialog("", "Delete failed: " + ex.Message, MessageBoxButtons.OK, MessageType.Info);
            }
        }
    }
}

using PitneyBowesCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace PitneyBowesCalculator
{
    public partial class ViewWOListDialog : Form
    {
        private List<WorkOrder> _items;
        public List<WorkOrder> DeletedItems { get; private set; }
        private string jobname;
        private string jobn;
        private int _jobId;

        // ✅ Fix — cancel parent timer before we take action
        private System.Threading.CancellationTokenSource _externalCts;
        public void SetCancellationSource(System.Threading.CancellationTokenSource cts)
        {
            _externalCts = cts;
        }

        public ViewWOListDialog()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this);
            ShadowHelper.ApplyShadow(this);
            this.KeyPreview = true;
            this.KeyDown += RoundedModal_KeyDown;
        }

        private void RoundedModal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        public void SetItems(string jobName, string jobNum, int jobId, IEnumerable<WorkOrder> items)
        {
            jobname = jobName;
            jobn = jobNum;
            _jobId = jobId;
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

        public void RefreshItems(List<WorkOrder> items)
        {
            SetItems(jobname, jobn, _jobId, items);
        }

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

            // ✅ Fix — cancel the parent timer before we act, prevents double-prompt
            _externalCts?.Cancel();

            DeletedItems = selectedRows
                .Select(r => r.BoundItem)
                .ToList();

            try
            {
                var ids = DeletedItems.Select(w => w.Id).ToList();
                var palletId = DeletedItems.First().PalletId;

                // Guard: re-check pallet state from DB before deleting anything
                var freshJob = await RqliteClient.LoadSingleJobGraphAsync(_jobId);
                var freshPallet = freshJob?.Pallets.FirstOrDefault(p => p.PalletId == palletId);

                if (freshPallet == null || freshPallet.PackedAt != null)
                {
                    MessageDialogBox.ShowDialog(
                        "Pallet Already Packed",
                        "This pallet was packed by another workstation. Changes cannot be made.",
                        MessageBoxButtons.OK,
                        MessageType.Warning
                    );
                    this.Close();
                    return;
                }

                // Single atomic call
                await RqliteClient.DeleteWorkOrdersAndMaybePalletAsync(ids, palletId);
                var savedJob = await RqliteClient.LoadSingleJobGraphAsync(_jobId);
                if (savedJob?.LastUpdatedRaw != null)
                    PBCMain.Instance.MarkPendingUpdate(_jobId, savedJob.LastUpdatedRaw);

                // Update UI locally (optimistic)
                foreach (var wo in DeletedItems)
                    _items.Remove(wo);

                // If empty, just close
                if (!_items.Any())
                {
                    MessageDialogBox.ShowDialog("",
                        "No work orders left. Pallet will be removed.",
                        MessageBoxButtons.OK,
                        MessageType.Info);

                    this.Close();
                    return;
                }

                // Rebind UI
                SetItems(jobname, jobn, _jobId, _items);
            }
            catch (Exception ex)
            {
                Utils.WriteExceptionError(ex);
                MessageDialogBox.ShowDialog("", "Delete failed: " + ex.Message, MessageBoxButtons.OK, MessageType.Info);
            }
        }
    }
}
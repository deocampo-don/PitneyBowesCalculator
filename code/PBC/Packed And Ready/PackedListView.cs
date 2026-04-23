using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PitneyBowesCalculator.Picked_Up;

namespace PitneyBowesCalculator.Packed_And_Ready
{
    public partial class PackedListView : UserControl
    {

        public event EventHandler RowSelectionChanged;
        public PackedListView()
        {
            InitializeComponent();

        }
        public event EventHandler<PbJobModel> PackedDataChanged;
        public void SetItems(IEnumerable<PbJobModel> items)
        {
            packedFlowRow.SuspendLayout();         

            foreach (Control c in packedFlowRow.Controls)
                c.Dispose();
            packedFlowRow.Controls.Clear();

            foreach (var job in items.ToList())
                AddRow(CreateRow(job));            

            packedFlowRow.ResumeLayout();
        }

        public void BeginUpdate()
        {
            this.SuspendLayout();
        }

        public void EndUpdate()
        {
            this.ResumeLayout();
        }


        public void AddRow(PackedRowControl row)
        {
            //   row.Width = packedScrollHost.ClientSize.Width -90; // leave room for scrollbar
            row.Margin = new Padding(5, 5, 5, 5);
            packedFlowRow.Controls.Add(row);
        }

        public List<PbJobModel> GetReadyJobs()
        {
            return packedFlowRow.Controls
                .OfType<PackedRowControl>()
                .Select(r => r.BoundJob)
                .Where(job =>
                {
                    var activePallets = job.Pallets?
                        .Where(p =>
                            p.State != PalletState.NotReady &&
                            p.State != PalletState.Shipped)
                        .ToList();

                    return activePallets != null &&
                           activePallets.Count > 0 &&
                           activePallets.All(p =>
                               p.State == PalletState.Ready &&
                               p.PackedAt.HasValue);
                })
                .ToList();
        }

        public void SetAllSelected(bool isSelected)
        {

            foreach (Control c in packedFlowRow.Controls)
            {
                if (c is PackedRowControl row)
                {
                    row.SetChecked(isSelected);
                }
            }

        }
        public int GetScrollPosition()
        {
            return packedFlowRow.AutoScrollPosition.Y * -1;
        }

        public void SetScrollPosition(int y)
        {
            packedFlowRow.AutoScrollPosition = new Point(0, y);
        }

        private PackedRowControl CreateRow(PbJobModel job)
        {
            var row = new PackedRowControl();
            row.Bind(job);
            row.ViewDialogClosed += (_, __) => PackedDataChanged?.Invoke(this, job);
            row.SelectionChanged += (_, __) => RowSelectionChanged?.Invoke(this, EventArgs.Empty);
            return row;
        }
        public void RemoveItem(int jobId)
        {
            var row = packedFlowRow.Controls
                .OfType<PackedRowControl>()
                .FirstOrDefault(r => r.BoundJob.JobId == jobId);

            if (row != null)
            {
                packedFlowRow.SuspendLayout();   // optional but consistent
                packedFlowRow.Controls.Remove(row);
                row.Dispose();
                packedFlowRow.ResumeLayout();
            }
        }

        public void AddItem(PbJobModel job)
        {
            packedFlowRow.Controls.Add(CreateRow(job)); 
        }

        public int GetItemIndex(int jobId)
        {
            var controls = packedFlowRow.Controls.OfType<PackedRowControl>().ToList();
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].BoundJob?.JobId == jobId)
                    return i;
            }
            return -1;
        }

        public bool AllChecked()
        {
            var rows = packedFlowRow.Controls.OfType<PackedRowControl>().ToList();
            return rows.Count > 0 && rows.All(r => r.IsChecked);
        }

        public void InsertItem(PbJobModel job, int index)
        {
            var row = CreateRow(job);
            packedFlowRow.SuspendLayout();       // ✅ add
            packedFlowRow.Controls.Add(row);
            packedFlowRow.Controls.SetChildIndex(row, index);
            packedFlowRow.ResumeLayout();        // ✅ add
        }

    }
}

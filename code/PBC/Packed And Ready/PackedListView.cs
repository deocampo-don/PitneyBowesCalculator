using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PitneyBowesCalculator.Picked_Up;

namespace PitneyBowesCalculator.Packed_And_Ready
{
    public partial class PackedListView : UserControl
    {
        public PackedListView()
        {
            InitializeComponent();

        }
        public event EventHandler<PbJobModel> PackedDataChanged;

        //public void SetItems(IEnumerable<PbJobModel> items)
        //{
        //    packedFlowRow.SuspendLayout();
        //    packedFlowRow.Controls.Clear();

        //    var list = items.ToList();
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        var job = list[i]; // capture properly

        //        var row = new PackedRowControl();
        //        row.Bind(job);
        //        AddRow(row);

        //        row.ViewDialogClosed += (_, __) =>
        //        {
        //            PackedDataChanged?.Invoke(this, job);
        //        };
        //    }

        //    packedFlowRow.ResumeLayout();
        //}
        public void SetItems(IEnumerable<PbJobModel> items)
        {
            packedFlowRow.SuspendLayout();
            packedFlowRow.Controls.Clear();

            var list = items.ToList();

            for (int i = 0; i < list.Count; i++)
            {
                var job = list[i]; // capture correctly

                var row = new PackedRowControl();
                row.Bind(job);

                row.ViewDialogClosed += (_, __) =>
                {
                    PackedDataChanged?.Invoke(this, job);
                };

                AddRow(row);
            }

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

        public void RemoveItem(int jobId)
        {
            var row = packedFlowRow.Controls
                .OfType<PackedRowControl>()
                .FirstOrDefault(r => r.BoundJob.JobId == jobId);

            if (row != null)
                packedFlowRow.Controls.Remove(row);
        }

        public void AddItem(PbJobModel job)
        {
            var row = new PackedRowControl();
            row.Bind(job);

            row.ViewDialogClosed += (_, __) =>
            {
                PackedDataChanged?.Invoke(this, job);
            };

            packedFlowRow.Controls.Add(row);
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

        public void InsertItem(PbJobModel job, int index)
        {
            var row = new PackedRowControl();
            row.Bind(job);

            row.ViewDialogClosed += (_, __) =>
            {
                PackedDataChanged?.Invoke(this, job);
            };

            packedFlowRow.Controls.Add(row);
            packedFlowRow.Controls.SetChildIndex(row, index);
        }

    }
}

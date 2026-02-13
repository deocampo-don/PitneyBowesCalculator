using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Picked_Up;

namespace WindowsFormsApp1.Packed_And_Ready
{
    public partial class PackedListView : UserControl
    {
        public PackedListView()
        {
            InitializeComponent();

        }
        public event EventHandler<PbJobModel> PackedDataChanged;

        public void SetItems(IEnumerable<PbJobModel> items)
        {
            packedFlowRow.SuspendLayout();
            packedFlowRow.Controls.Clear();

            var list = items.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var job = list[i]; // capture properly

                var row = new PackedRowControl();
                row.Bind(job);
                AddRow(row);

                row.ViewDialogClosed += (_, __) =>
                {
                    PackedDataChanged?.Invoke(this, job);
                };
            }



            packedFlowRow.ResumeLayout(); 
        }
   

        private void ResizeCards()
        {
            if (packedFlowRow.Controls.Count == 0)
                return;

            int scrollbarWidth = SystemInformation.VerticalScrollBarWidth;

            bool hasScrollbar = packedFlowRow.VerticalScroll.Visible;

            int availableWidth = packedFlowRow.ClientSize.Width
                                 - (hasScrollbar ? scrollbarWidth : 0);

            int cardsPerRow = 3;
            int spacing = 20;

            int cardWidth = (availableWidth / cardsPerRow) - spacing;

            foreach (Control c in packedFlowRow.Controls)
            {
                c.Width = cardWidth;
            }
        }


        public void SetAllReadyToShip(bool isReady)
        {
            foreach (Control c in packedFlowRow.Controls)
            {
                if (c is PackedRowControl row)
                {
                    row.SetReadyToShip(isReady);
                }
            }
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
                .Where(r => r.IsReady())
                .Select(r => r.GetModel())
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


        public void RefreshItem(PbJobModel job)
        {
            var row = packedFlowRow.Controls
                .OfType<PackedRowControl>()
                .FirstOrDefault(r => r.BoundJob?.JobId == job.JobId);

            if (row != null)
                row.Bind(job);
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



    }
}

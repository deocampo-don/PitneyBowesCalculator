using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Packed_And_Ready;

namespace WindowsFormsApp1.Picked_Up
{
    public partial class PickedUpListView : UserControl
    {
        public PickedUpListView()
        {
            InitializeComponent();
        }
       
        public event EventHandler<PbJobModel> PalletChanged;

        public void RefreshItem(PbJobModel job)
        {
            var row = pickflowRows.Controls
                .OfType<PickedUpRowControl>()
                .FirstOrDefault(r => r.BoundJob.JobId == job.JobId);

            if (row != null)
                row.Bind(job);
        }
        public void BeginUpdate()
        {
            this.SuspendLayout();
        }

        public void EndUpdate()
        {
            this.ResumeLayout();
        }
        public void RemoveItem(int jobId)
        {
            var row = pickflowRows.Controls
                .OfType<PickedUpRowControl>()
                .FirstOrDefault(r => r.BoundJob.JobId == jobId);

            if (row != null)
                pickflowRows.Controls.Remove(row);
        }

        public void AddItem(PbJobModel job)
        {
            var row = new PickedUpRowControl();
            row.Bind(job);

            pickflowRows.Controls.Add(row);
            BeginInvoke(new Action(ResizeRowsToHost));
        }

        public void SetAllSelected(bool isSelected)
        {
            foreach (var row in pickflowRows.Controls.OfType<PickedUpRowControl>())
            {
                row.IsChecked = isSelected;
            }
        }

        public IEnumerable<PbJobModel> GetAllJobs()
        {
            return pickflowRows.Controls
                .OfType<PickedUpRowControl>()
                .Select(r => r.BoundJob);
        }
        public void SetItems(IEnumerable<PbJobModel> items)
        {
            pickflowRows.SuspendLayout();

            pickflowRows.Controls.Clear();

            // Give the scrollbar its own space so rows can fill visually
          
            foreach (var item in items)
            {
                var row = new PickedUpRowControl();
                row.Bind(item);
                pickflowRows.Controls.Add(row);
            }

            pickflowRows.ResumeLayout();
            BeginInvoke(new Action(ResizeRowsToHost));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeRowsToHost();
        }


        public void ResizeRowsToHost()
        {
            int width = pickflowRows.ClientSize.Width;

            foreach (Control c in pickflowRows.Controls)
            {
                c.Width = width;
            }
        }
        public IEnumerable<PbJobModel> GetSelectedJobs()
        {
            return pickflowRows.Controls
                .OfType<PickedUpRowControl>()
                .Where(row => row.IsChecked)
                .Select(row => row.BoundJob);
        }
    }
}

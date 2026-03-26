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

        private readonly Dictionary<int, List<PickedUpRowControl>> _rowsByJobId = new();
        public PickedUpListView()
        {
            InitializeComponent();
        }
       
        public event EventHandler<PbJobModel> PalletChanged;

        public void RefreshItem(PbJobModel job)
        {
            if (_rowsByJobId.TryGetValue(job.JobId, out var rows))
            {
                foreach (var row in rows)
                {
                    row.Bind(job);
                }
            }
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
            RemoveItemsByJobId(jobId); // reuse logic
        }

        public void AddItem(PbJobModel job)
        {
            // 🔥 prevent duplicates
            if (_rowsByJobId.ContainsKey(job.JobId))
            {
                RemoveItemsByJobId(job.JobId);
            }

            var row = new PickedUpRowControl();
            row.Bind(job);

            pickflowRows.Controls.Add(row);

            if (!_rowsByJobId.TryGetValue(job.JobId, out var list))
            {
                list = new List<PickedUpRowControl>();
                _rowsByJobId[job.JobId] = list;
            }
            list.Add(row);

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
            _rowsByJobId.Clear(); // 🔥 reset index

            foreach (var item in items)
            {
                var row = new PickedUpRowControl();
                row.Bind(item);

                pickflowRows.Controls.Add(row);

                if (!_rowsByJobId.TryGetValue(item.JobId, out var list))
                {
                    list = new List<PickedUpRowControl>();
                    _rowsByJobId[item.JobId] = list;
                }
                list.Add(row);
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

        public void RemoveItemsByJobId(int jobId)
        {
            if (!_rowsByJobId.TryGetValue(jobId, out var rows))
                return;

            foreach (var row in rows)
            {
                pickflowRows.Controls.Remove(row);
                row.Dispose();
            }

            _rowsByJobId.Remove(jobId);
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

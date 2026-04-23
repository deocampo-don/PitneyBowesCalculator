using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PitneyBowesCalculator.Packed_And_Ready;

namespace PitneyBowesCalculator.Picked_Up
{
    public partial class PickedUpListView : UserControl
    {

        private readonly Dictionary<int, List<PickedUpRowControl>> _rowsByJobId = new();
        private int _lastResizeWidth = -1;
        public PickedUpListView()
        {
            InitializeComponent();
			pickflowRows.Resize += (_, __) => ResizeRowsToHost();
		}
       
        public event EventHandler<PbJobModel> PalletChanged;

		public void BeginUpdate()
        {
            this.SuspendLayout();
        }

        public void EndUpdate()
        {
            this.ResumeLayout();
        }
		public void AddItem(PbJobModel job)
		{
			if (!_rowsByJobId.TryGetValue(job.JobId, out var list))
			{
				list = new List<PickedUpRowControl>();
				_rowsByJobId[job.JobId] = list;
			}

            // 🔥 Prevent duplicates (same shipment)
            bool exists = list.Any(r =>
    r.BoundJob?.ShippedDate == job.ShippedDate);

            if (exists)
				return;

			var row = new PickedUpRowControl();
			row.Bind(job);

			pickflowRows.Controls.Add(row);
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
            foreach (Control c in pickflowRows.Controls)
                c.Dispose();

           
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
            if (width == _lastResizeWidth) return;   // ✅ add
            _lastResizeWidth = width;                 // ✅ add

            pickflowRows.SuspendLayout();             // ✅ add
            foreach (Control c in pickflowRows.Controls)
                c.Width = width;
            pickflowRows.ResumeLayout();              // ✅ add
        }
        public void SortByShippedDateDescending()
        {
            // Get all rows with their bound job's shipped date
            var rows = pickflowRows.Controls
                .OfType<PickedUpRowControl>()
                .OrderByDescending(r => r.BoundJob?.ShippedDate)
                .ToList();

            if (!rows.Any())
                return;

            pickflowRows.SuspendLayout();

            // Reorder controls in the FlowLayoutPanel
            for (int i = 0; i < rows.Count; i++)
            {
                pickflowRows.Controls.SetChildIndex(rows[i], i);
            }

            pickflowRows.ResumeLayout();
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

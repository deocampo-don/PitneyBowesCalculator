using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        //public void AddRow(PickedUpRowControl row)
        //{
        //    row.Width = pickedScrollHost.ClientSize.Width - 25; // leave room for scrollbar
        //    row.Margin = new Padding(12, 10, 12, 0);
        //    pickflowRows.Controls.Add(row);
        //}

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
        private void pickedScrollHost_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

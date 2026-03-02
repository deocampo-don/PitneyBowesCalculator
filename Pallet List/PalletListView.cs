using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class PalletListView : UserControl
    {
        public event EventHandler<PbJobModel> DeleteRequested;
        public event EventHandler<PbJobModel> PalletChanged;

        public PalletListView()
        {
            InitializeComponent();
            scrollHost.Resize += (_, __) => ResizeRowsToHost();
            

        }

        public void RefreshItem(PbJobModel job)
        {
            var row = flowRows.Controls
          .OfType<PalletRowControl>()
          .FirstOrDefault(r => r.BoundJob?.JobId == job.JobId);

            if (row != null)
                row.Bind(job);
        }

        public void RemoveItem(int jobId)
        {
            var row = flowRows.Controls
                .OfType<PalletRowControl>()
                .FirstOrDefault(r => r.BoundJob.JobId == jobId);

            if (row != null)
                flowRows.Controls.Remove(row);
        }
        public void AddItem(PbJobModel job)
        {
            var row = new PalletRowControl();
            row.Bind(job);

            row.DeleteRequested += (_, j) =>
                DeleteRequested?.Invoke(this, j);

            row.PalletChanged += (_, j) =>
                PalletChanged?.Invoke(this, j);

            flowRows.Controls.Add(row);
            BeginInvoke(new Action(ResizeRowsToHost));
        }

        public void BeginUpdate()
        {
            this.SuspendLayout();
        }

        public void EndUpdate()
        {
            this.ResumeLayout();
        }
        public void SetItems(IEnumerable<PbJobModel> items)
        {
            flowRows.SuspendLayout();
            flowRows.Controls.Clear();

            var list = items.ToList();

            for (int i = 0; i < list.Count; i++)
            {
                var row = new PalletRowControl();
                row.Bind(list[i]);

                // 🔑 Forward Delete event
                row.DeleteRequested += (_, job) =>
                {
                    DeleteRequested?.Invoke(this, job);
                };

                // 🔔 Forward PalletChanged event
                row.PalletChanged += (_, job) =>
                {
                    PalletChanged?.Invoke(this, job);
                };

                flowRows.Controls.Add(row);
            }

            flowRows.ResumeLayout();
            BeginInvoke(new Action(ResizeRowsToHost));
        
        }

   

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeRowsToHost();
        }

        public void ResizeRowsToHost()
        {
            int width = scrollHost.ClientSize.Width;

            foreach (Control c in flowRows.Controls)
            {
                c.Width = width-5; // small margin
            }
        }

        private void scrollHost_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

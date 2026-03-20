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
        public event EventHandler<PbJobModel> EditRequested;

        public PalletListView()
        {
            InitializeComponent();
            scrollHost.Resize += (_, __) => ResizeRowsToHost();
           
        }

        public void UpdateItem(PbJobModel job)
        {
            var existingControl = FindControl(job.JobId);

            if (existingControl != null)
            {
                // ✅ Update data WITHOUT removing
                existingControl.Bind(job);
            }
            else
            {
                // fallback (should rarely happen)
                AddItem(job);
            }
        }

        private PalletRowControl FindControl(int jobId)
        {
            foreach (Control ctrl in rowsContainer.Controls)
            {
                if (ctrl is PalletRowControl row && row.BoundJob?.JobId == jobId)
                {
                    return row;
                }
            }
            return null;
        }
        public void RefreshItem(PbJobModel job)
        {
            var row = rowsContainer.Controls
          .OfType<PalletRowControl>()
          .FirstOrDefault(r => r.BoundJob?.JobId == job.JobId);

            if (row != null)
                row.Bind(job);
        }

        public void RemoveItem(int jobId)
        {
            var row = rowsContainer.Controls
                .OfType<PalletRowControl>()
                .FirstOrDefault(r => r.BoundJob?.JobId == jobId);

            if (row != null)
                rowsContainer.Controls.Remove(row);
        }
        public void AddItem(PbJobModel job)
        {
            var row = new PalletRowControl();
            row.Bind(job);

            row.DeleteRequested += async (_, j) =>
            {
                try
                {
                    DeleteRequested?.Invoke(this, j);
                }
                catch (Exception ex)
                {
                    Utils.WriteUnexpectedError("DeleteRequested event failed");
                    Utils.WriteExceptionError(ex);
                }
            };

            row.PalletChanged += (_, j) =>
                PalletChanged?.Invoke(this, j);
            row.EditRequested += (_, j) =>
        EditRequested?.Invoke(this, j);
            row.Dock = DockStyle.Top;

            rowsContainer.Controls.Add(row);
            rowsContainer.Controls.SetChildIndex(row, 0); // newest on top
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
            rowsContainer.SuspendLayout();
            rowsContainer.Controls.Clear();

            foreach (var job in items)
            {
                AddItem(job);
            }

            rowsContainer.ResumeLayout();
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeRowsToHost();
        }

        public void ResizeRowsToHost()
        {
            int width = rowsContainer.ClientSize.Width;

            foreach (Control c in rowsContainer.Controls)
            {
                c.Width = width-5; // small margin
            }
        }

      
    }
}

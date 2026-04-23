using PitneyBowesCalculator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PitneyBowesCalculator
{
    public partial class PalletListView : UserControl
    {
        public event EventHandler<PbJobModel> DeleteRequested;
        public event EventHandler<PbJobModel> PalletChanged;
        public event EventHandler<PbJobModel> EditRequested;
        public event EventHandler<PbJobModel> SoftDeleteRequested;
        private int _lastResizeWidth = -1;

        public PalletListView()
        {
            InitializeComponent();
            scrollHost.Resize += (_, __) => ResizeRowsToHost();
           
        }

        public void RemoveItem(int jobId)
        {
            var row = rowsContainer.Controls
                .OfType<PalletRowControl>()
                .FirstOrDefault(r => r.BoundJob?.JobId == jobId);

            if (row == null) return;

            // ✅ Suspend layout during remove to prevent scroll reset
            rowsContainer.SuspendLayout();
            rowsContainer.Controls.Remove(row);
            row.Dispose();
            rowsContainer.ResumeLayout();
        }
        public void AddItem(PbJobModel job)
        {
            var row = CreateRow(job);
            rowsContainer.SuspendLayout();        
            rowsContainer.Controls.Add(row);
            rowsContainer.Controls.SetChildIndex(row, 0);
            rowsContainer.ResumeLayout();       
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

            // ✅ Dispose before clearing
            foreach (Control c in rowsContainer.Controls)
                c.Dispose();

            rowsContainer.Controls.Clear();

            foreach (var job in items)
                AddItem(job);

            rowsContainer.ResumeLayout();
        }

        public int GetItemIndex(int jobId)
        {
            var controls = rowsContainer.Controls.OfType<PalletRowControl>().ToList();
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].BoundJob?.JobId == jobId)
                    return i;
            }
            return -1;
        }

        public void InsertItem(PbJobModel job, int index)
        {
            var row = CreateRow(job);
            rowsContainer.SuspendLayout();
            rowsContainer.Controls.Add(row);
            rowsContainer.Controls.SetChildIndex(row, index);
            rowsContainer.ResumeLayout();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeRowsToHost();
        }

        private PalletRowControl CreateRow(PbJobModel job)
        {
            var row = new PalletRowControl();
            row.Bind(job);
            row.Dock = DockStyle.Top;

            row.DeleteRequested += (_, j) =>
            {
                try { DeleteRequested?.Invoke(this, j); }
                catch (Exception ex)
                {
                    Utils.WriteUnexpectedError("DeleteRequested event failed");
                    Utils.WriteExceptionError(ex);
                }
            };
            row.PalletChanged += (_, j) => PalletChanged?.Invoke(this, j);
            row.EditRequested += (_, j) => EditRequested?.Invoke(this, j);
            row.SoftDeleteRequested += (_, j) => SoftDeleteRequested?.Invoke(this, j);

            return row;
        }

        

        public void ResizeRowsToHost()
        {
            int width = rowsContainer.ClientSize.Width;
            if (width == _lastResizeWidth) return;
            _lastResizeWidth = width;

            rowsContainer.SuspendLayout();
            foreach (Control c in rowsContainer.Controls)
                c.Width = width - 5;
            rowsContainer.ResumeLayout();
        }

        public int GetScrollPosition()
        {
            // ✅ KryptonPanel wraps a standard panel internally
            // AutoScrollPosition.Y is negative, so negate it
            return scrollHost.AutoScrollPosition.Y * -1;
        }

        public void SetScrollPosition(int y)
        {
            // ✅ Defer until after layout is fully computed
            BeginInvoke(new Action(() =>
            {
                scrollHost.AutoScrollPosition = new Point(0, y);
                scrollHost.AutoScrollPosition = new Point(0, y); // double set for WinForms quirk
            }));
        }
    }
}

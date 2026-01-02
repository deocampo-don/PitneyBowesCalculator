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

        public void SetItems(IEnumerable<PickListModel> items)
        {
            pickflowRows.SuspendLayout();
            pickflowRows.Controls.Clear();

            var list = items.ToList();

            for (int i = 0; i < list.Count; i++)
            {
                var row = new PickedUpRowControl();
                row.Bind(list[i]);


                pickflowRows.Controls.Add(row);
            }

            pickflowRows.ResumeLayout();
        }
        public void AddRow(PickedUpRowControl row)
        {
            row.Width = pickedScrollHost.ClientSize.Width - 25; // leave room for scrollbar
            row.Margin = new Padding(12, 10, 12, 0);
            pickflowRows.Controls.Add(row);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            foreach (Control c in pickflowRows.Controls)
                c.Width = pickedScrollHost.ClientSize.Width - 25;
        }
        private void pickedScrollHost_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

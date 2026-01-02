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
        public PalletListView()
        {
            InitializeComponent();

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
         

                flowRows.Controls.Add(row);
            }

            flowRows.ResumeLayout();
        }
        public void AddRow(PalletRowControl row)
        {
            row.Width = scrollHost.ClientSize.Width - 25; // leave room for scrollbar
            row.Margin = new Padding(12, 10, 12, 0);
            flowRows.Controls.Add(row);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            foreach (Control c in flowRows.Controls)
                c.Width = scrollHost.ClientSize.Width - 25;
        }

        private void scrollHost_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

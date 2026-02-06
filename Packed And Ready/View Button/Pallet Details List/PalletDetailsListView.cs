using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button.Pallets_Details
{
    public partial class PalletDetailsListView : UserControl
    {
       
        public PalletDetailsListView()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

        }

        public void SetItems(IEnumerable<WorkOrder> items)
        {
            flowRow.SuspendLayout();
            flowRow.Controls.Clear();

            if (items == null)
            {
                flowRow.ResumeLayout();
                return;
            }

            int index = 0;
            var list = items.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var row = new PalletDetailsRowControl();
                row.Bind(list[i]);


                // ✅ Alternate background color
                row.BackColor = (index % 2 == 0)
                    ? Color.White
                    : Color.FromArgb(245, 245, 245); // light gray
                   

                AddRow(row);
                index++;
            }

            flowRow.ResumeLayout();
        }
        public void AddRow(PalletDetailsRowControl row)
        {
            //   row.Width = packedScrollHost.ClientSize.Width -90; // leave room for scrollbar
            row.Margin = new Padding();
            flowRow.Controls.Add(row);
        }
    }

}


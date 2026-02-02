using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready
{
    public partial class PackedListView : UserControl
    {
        public PackedListView()
        {
            InitializeComponent();
        }

        public void SetItems(IEnumerable<PickListModel> items)
        {
            packedFlowRow.SuspendLayout();
            packedFlowRow.Controls.Clear();

            var list = items.ToList();

            for (int i = 0; i < list.Count; i++)
            {
                var row = new PackedRowControl();
                row.Bind(list[i]);


                packedFlowRow.Controls.Add(row);
            }

            packedFlowRow.ResumeLayout();
        }
        public void AddRow(PackedRowControl row)
        {
            row.Width = packedScrollHost.ClientSize.Width - 25; // leave room for scrollbar
            //  row.Margin = new Padding(12, 10, 12, 0);
            packedFlowRow.Controls.Add(row);
        }
    }
}

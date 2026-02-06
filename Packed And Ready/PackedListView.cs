using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready
{
    public partial class PackedListView : UserControl
    {
        public PackedListView()
        {
            InitializeComponent();
        }
        public event EventHandler ItemsChanged;
        public void SetItems(IEnumerable<PbJobModel> items)
        {
            packedFlowRow.SuspendLayout();
            packedFlowRow.Controls.Clear();

            var list = items.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var row = new PackedRowControl();
                row.Bind(list[i]);
                AddRow(row);

                row.ViewDialogClosed += (_, __) =>
                {
                    ItemsChanged?.Invoke(this, EventArgs.Empty);
                };


            }


            packedFlowRow.ResumeLayout();
        }


        public void SetAllReadyToShip(bool isReady)
        {
            foreach (Control c in packedFlowRow.Controls)
            {
                if (c is PackedRowControl row)
                {
                    row.SetReadyToShip(isReady);
                }
            }
        }



        public void AddRow(PackedRowControl row)
        {
            //   row.Width = packedScrollHost.ClientSize.Width -90; // leave room for scrollbar
            row.Margin = new Padding(5, 5, 5, 5);
            packedFlowRow.Controls.Add(row);
        }
    }
}


using System.Windows.Forms;

namespace PitneyBowesCalculator.Packed_And_Ready.View_Button
{
    public partial class PalletDetailsRowControl : UserControl
    {
        private WorkOrder _workorder;
        public PalletDetailsRowControl()
        {
            InitializeComponent();
           
        }

        public void Bind(WorkOrder workorder)
        {
            if (workorder == null)
                return;

            _workorder = workorder;

            // ✅ Display WO Code
            txtWOName.Text = _workorder.WorkOrderCode ?? string.Empty;

            // ✅ Display Envelope Qty (formatted)
            txtValue.Text = _workorder.Quantity.ToString("N0");

            // Optional: checkbox
           // chkSelected.Checked = _workorder.IsSelected;



        }


    }
}

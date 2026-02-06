
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button
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
            txtWOName.Text = _workorder.Code ?? string.Empty;

            // ✅ Display Envelope Qty (formatted)
            txtValue.Text = _workorder.EnvelopeQty.ToString("N0");

            // Optional: checkbox
           // chkSelected.Checked = _workorder.IsSelected;



        }


    }
}

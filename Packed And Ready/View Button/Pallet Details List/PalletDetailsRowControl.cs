
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



        }


    }
}

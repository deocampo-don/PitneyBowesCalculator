using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class PalletNumList : UserControl
    {
        public PalletNumList()
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;

            CSSDesign.AddPanelBorder(pnlMain, Color.Silver, 1);
        }

    }
}

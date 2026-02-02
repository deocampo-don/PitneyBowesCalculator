using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class ViewButtonDialog : Form
    {

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private int _formRadius = 12;
        public ViewButtonDialog()
        {
            InitializeComponent();
            LoadPalletNumList();
            LoadPalletDetailsRowControl();
            // this.StartPosition = FormStartPosition.CenterScreen;



            pnlHeader.MouseDown += pnlHeader_MouseDown;

            this.Paint += ViewButtonDialog_Paint;

            CSSDesign.MakeRounded(btnRemovePallets, 10);
            CSSDesign.MakeRounded(btnPrintPallets, 10);
            CSSDesign.AddPanelBorder(pnlDashboard, Color.Silver, 1);
            CSSDesign.AddPanelBorder(pnlDetails, Color.Silver, 1);


        }
        private void LoadPalletNumList()
        {
            pnlPalletNoList.Controls.Clear(); // remove anything already there

            PalletNumListRowControl palletList = new PalletNumListRowControl
            {
                Dock = DockStyle.Fill
            };

            pnlPalletNoList.Controls.Add(palletList);
        }
        private void LoadPalletDetailsRowControl()
        {
            pnlDetails.Controls.Clear(); // remove anything already there

            PalletDetailsRowControl palletDetails = new PalletDetailsRowControl
            {
                
            };

            pnlDetails.Controls.Add(palletDetails);
        }

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture(); // release the mouse
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0); // move the form
            }
        }

        private void ViewButtonDialog_Paint(object sender, PaintEventArgs e)
        {
            CSSDesign.PaintRoundedForm(
               this,
               e,
               _formRadius,
               Color.Gray

           );
        }

        private void btnRemovePallets_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm(); // get the dashboard form

            using (RemovePallets removepallets = new RemovePallets())
            {
                removepallets.ShowDialog(parentForm); // locks the dashboard correctly
            }
        }


        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

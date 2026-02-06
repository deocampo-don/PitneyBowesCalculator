
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class RemovePallets : Form
    {
        private int _formRadius = 12;

        [DllImport("user32.dll")] private static extern bool ReleaseCapture();
        [DllImport("user32.dll")] private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        public RemovePallets()
        {
            InitializeComponent();
            btnNo.Focus();

            CSSDesign.ApplyRoundedCorners(this, 20);
            Paint += RemovePallets_Paint;

            pnlHeader.MouseDown += pnlHeader_MouseDown;

            CSSDesign.MakeRounded(btnNo, 15);
            CSSDesign.MakeRounded(btnYes, 15);
        }



        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);

          
            }
        }

        private void RemovePallets_Paint(object sender, PaintEventArgs e)
        {
            CSSDesign.PaintRoundedForm(this, e, _formRadius, Color.Gray);
        }

        private void btnNo_Click_1(object sender, EventArgs e) => Close();

     

        private void btnYes_Click_1(object sender, EventArgs e)
        {
            // ✅ Important: return Yes so parent knows to proceed
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

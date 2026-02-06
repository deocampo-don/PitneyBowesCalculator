using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{

    public partial class ShipPalletsConfirmationDialog : Form
    {

        [DllImport("user32.dll")] private static extern bool ReleaseCapture();
        [DllImport("user32.dll")] private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        private int _formRadius = 12;
        public ShipPalletsConfirmationDialog()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent; // optional: ensure centered
            CSSDesign.ApplyRoundedCorners(this, 20);
            CSSDesign.MakeRounded(btnNo, 15);
            CSSDesign.MakeRounded(btnYes, 15);
            Paint += RemovePallets_Paint;

            pnlHeader.MouseDown += pnlHeader_MouseDown;

        }

        private void RemovePallets_Paint(object sender, PaintEventArgs e)
        {
            CSSDesign.PaintRoundedForm(this, e, _formRadius, Color.Gray);
        }


        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }


        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;  // ✅ confirm
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;  // ✅ cancel
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;  // ✅ also cancel
            this.Close();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

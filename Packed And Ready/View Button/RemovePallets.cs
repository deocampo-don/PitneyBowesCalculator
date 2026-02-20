
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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
            

            pnlHeader.MouseDown += pnlHeader_MouseDown;


            btnCancel1.Visible = false;
        }



        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

    

       
        private bool firstYesClicked = false;

        private async void btnYes1_Click(object sender, EventArgs e)
        {
            // SECOND CLICK OF YES → RETURN DialogResult.Yes
            if (firstYesClicked)
            {
                //Merge function here

                // this.Close();
                return;
            }

            // FIRST CLICK OF YES → RUN ANIMATION
            firstYesClicked = true;
            int targetHeight = 330;
            int step = 10;            // how many pixels per frame
            int delay = 3;           // delay per frame (ms)

            int bottom = this.Top + this.Height;

            while (this.Height < targetHeight)
            {

                int nextHeight = Math.Min(this.Height + step, targetHeight);
                this.Height = nextHeight;
                this.Top = bottom - this.Height;  // move top up so bottom stays put
                await Task.Delay(delay);

            }

            //New Labels
            lblHeader.Text = "Hold On!";
            label1.Text = "You already have a pallet in progress. Would you like";
            label2.Text = "to merge this to that ongoing pallet";

            label3.Text = "Yes - Merge to the ongoing pallet.";
            label4.Text = "No - Delete the pallet.";
            label5.Text = "Cancel - Finish packing the current pallet.";

            // Ensure exact final geometry
            this.Height = targetHeight;
            this.Top = bottom - this.Height;

            btnCancel1.Visible = true;

        }

        private void btnNo1_Click(object sender, EventArgs e)
        {

            // SECOND CLICK OF YES → RETURN DialogResult.Yes
            if (firstYesClicked)
            {
                //Delete the pallet
                this.DialogResult = DialogResult.Yes;
                this.Close();
                return;
            }
            // FIRST CLICK OF YES → RUN ANIMATION
            firstYesClicked = true;
            this.Close();
        }

        private void btnCancel1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

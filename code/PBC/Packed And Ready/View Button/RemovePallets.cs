using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class RemovePallets : Form
    {
        /* -------------------------------------------------------------
         * ENUM – WHAT ACTION USER CHOSE
         * ------------------------------------------------------------- */
        public enum RemoveAction
        {
            None,
            UndoPack,
            Delete,
            Merge
        }
        

        /* -------------------------------------------------------------
         * PUBLIC RESULT
         * ------------------------------------------------------------- */
        public RemoveAction Action { get; private set; } = RemoveAction.None;

        /* -------------------------------------------------------------
         * STATE
         * ------------------------------------------------------------- */
        private readonly bool _hasActivePallet;
        private bool firstYesClicked = false;
       

        /* -------------------------------------------------------------
         * WIN32 (DRAG FORM)
         * ------------------------------------------------------------- */
        [DllImport("user32.dll")] private static extern bool ReleaseCapture();
        [DllImport("user32.dll")] private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        /* -------------------------------------------------------------
         * CONSTRUCTOR
         * ------------------------------------------------------------- */
        
        public RemovePallets(bool hasActivePallet)
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this);
            ShadowHelper.ApplyShadow(this);
            _hasActivePallet = hasActivePallet;


            

            pnlHeader.MouseDown += pnlHeader_MouseDown;

            // Default UI state
            btnCancel1.Visible = false;
            btnNo1.Visible = true;


            btnYes1.Text = "Unpack";
            btnNo1.Text = "Delete";

            if (!_hasActivePallet)
            {
                // No merge scenario
                btnNo1.Visible = true;
                
            }
        }

        /* -------------------------------------------------------------
         * HEADER DRAG
         * ------------------------------------------------------------- */
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        /* -------------------------------------------------------------
         * YES BUTTON
         * ------------------------------------------------------------- */
        private async void btnYes1_Click(object sender, EventArgs e)
        {
            // CASE 1: No active pallet → undo pack
            if (!_hasActivePallet)
            {
                Action = RemoveAction.UndoPack;
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            // CASE 2: First YES → show animation + merge options
            if (!firstYesClicked)
            {
                firstYesClicked = true;

                await RunExpandAnimationAsync();
                SwitchToMergeUI();
                
                return;
            }

            // CASE 3: Second YES → MERGE
            Action = RemoveAction.Merge;
            DialogResult = DialogResult.OK;
            Close();
        }

        /* -------------------------------------------------------------
         * NO BUTTON (DELETE)
         * ------------------------------------------------------------- */
        private void btnNo1_Click(object sender, EventArgs e)
        {
            // If user presses NO before merge options appear → cancel
            if (!_hasActivePallet || !firstYesClicked)
            {
                Action = RemoveAction.Delete;
                DialogResult = DialogResult.OK;
                Close();
                
            }

            // After animation → NO means delete
            Action = RemoveAction.Delete;
            DialogResult = DialogResult.OK;
            Close();
        }

        /* -------------------------------------------------------------
         * CANCEL / EXIT
         * ------------------------------------------------------------- */
        private void btnCancel1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /* -------------------------------------------------------------
         * ANIMATION
         * ------------------------------------------------------------- */
        //private async Task RunExpandAnimationAsync()
        //{
        //    int targetHeight = 330;
        //    int step = 10;
        //    int delay = 3;

        //    int bottom = Top + Height;

        //    while (Height < targetHeight)
        //    {
        //        Height = Math.Min(Height + step, targetHeight);
        //        Top = bottom - Height;

        //        await Task.Delay(delay);
        //    }
        //}
        private async Task RunExpandAnimationAsync()
        {
            int targetHeight = 370;
            int step = 10;
            int delay = 3;

            int bottom = Top + Height;

            while (Height < targetHeight)
            {
                Height = Math.Min(Height + step, targetHeight);
                Top = bottom - Height;

                await Task.Delay(delay);
            }

            // Force layout refresh
            groupBox1.Refresh();
            panel1.Refresh();
            Refresh();
        }
        /* -------------------------------------------------------------
         * SWITCH UI TO MERGE MODE
         * ------------------------------------------------------------- */
        private void SwitchToMergeUI()
        {
            lblHeader.Text = "Hold On!";
            label1.Text = "You already have a pallet in progress.";
            label2.Text = "What would you like to do?";

            label3.Text = "Merge - Merges the selected pallet(s) into the ongoing pallet";
            label4.Text = "Delete – Removes the selected pallet(s) from the job.";
            label5.Text = "Cancel - Continue packing the current pallet";

            btnCancel1.Visible = true;
            btnNo1.Text = "Delete";
            btnYes1.Text = "Merge";
        }
    }
}
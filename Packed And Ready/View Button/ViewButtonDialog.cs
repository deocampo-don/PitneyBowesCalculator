using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class ViewButtonDialog : Form
    {
        /* -------------------------------------------------------------
         * FIELDS & DATA STORAGE
         * ------------------------------------------------------------- */

        /// <summary>
        /// Job data passed from PackedRowControl.
        /// </summary>
        private PbJobModel _job;

        /* -------------------------------------------------------------
         * WIN32 IMPORTS (DRAGGABLE BORDERLESS FORM)
         * ------------------------------------------------------------- */

        [DllImport("user32.dll")] private static extern bool ReleaseCapture();
        [DllImport("user32.dll")] private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private int _formRadius = 12;


        /* -------------------------------------------------------------
         * CONSTRUCTOR
         * ------------------------------------------------------------- */
        public ViewButtonDialog(PbJobModel job)
        {
            InitializeComponent();

            _job = job;

            // Load header fields first
            LoadHeaderInfo();

            // Load pallet list number Buttons (left)
            palletNumListViewList1.SetItems(_job);

            // Load empty detail panel layout (right)
            LoadPalletDetailsRowControl();

            // Wire events
            pnlHeader.MouseDown += pnlHeader_MouseDown;
            this.Paint += ViewButtonDialog_Paint;

            // Styling
            CSSDesign.MakeRounded(btnRemovePallets, 10);
            CSSDesign.MakeRounded(btnPrintPallets, 10);
            CSSDesign.AddPanelBorder(pnlDashboard, Color.Silver, 1);
            CSSDesign.AddPanelBorder(pnlDetails, Color.Silver, 1);
        }


        /* -------------------------------------------------------------
         * HEADER INFO BINDING
         * ------------------------------------------------------------- */
        private void LoadHeaderInfo()
        {
            if (_job == null)
                return;

            txtPBJobName.Text = _job.JobName;
            txtPBJobNumber.Text = _job.JobNumber.ToString();
            txtPackDate.Text = _job.PackDate.ToString("MM/dd/yyyy");
        }


        /* -------------------------------------------------------------
         * PALLET DETAILS PANEL (RIGHT SIDE)
         * ------------------------------------------------------------- */
        private void LoadPalletDetailsRowControl()
        {
            // If you have a default layout for pallet details, load it here.
            // Uncomment when ready:
            // pnlDetails.Controls.Clear();
            // pnlDetails.Controls.Add(new PalletDetailsRowControl());
        }


        /* -------------------------------------------------------------
         * FORM DRAGGING
         * ------------------------------------------------------------- */
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }


        /* -------------------------------------------------------------
         * FORM CUSTOM BORDER PAINTING
         * ------------------------------------------------------------- */
        private void ViewButtonDialog_Paint(object sender, PaintEventArgs e)
        {
            CSSDesign.PaintRoundedForm(this, e, _formRadius, Color.Gray);
        }


        /* -------------------------------------------------------------
         * EVENTS
         * ------------------------------------------------------------- */
        public event Action<int> PalletClicked;

        private void btnRemovePallets_Click(object sender, EventArgs e)
        {
            using (var dlg = new RemovePallets())
            {
                dlg.ShowDialog(this);
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class ViewButtonDialog : Form
    {
        /* -------------------------------------------------------------
         * FIELDS & DATA STORAGE
         * ------------------------------------------------------------- */

        private PbJobModel _job;
        public bool DataChanged { get; private set; }

        /* -------------------------------------------------------------
         * WIN32 IMPORTS
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

            LoadHeaderInfo();
            palletNumListViewList1.SetItems(_job);
            palletNumListViewList1.PalletClicked += OnPalletClicked;

            pnlHeader.MouseDown += pnlHeader_MouseDown;
            this.Paint += ViewButtonDialog_Paint;

            CSSDesign.MakeRounded(btnRemovePallets, 10);
            CSSDesign.MakeRounded(btnPrintPallets, 10);
            CSSDesign.AddPanelBorder(pnlDashboard, Color.Silver, 1);
            CSSDesign.AddPanelBorder(pnlDetails, Color.Silver, 1);
        }

        /* -------------------------------------------------------------
         * HEADER
         * ------------------------------------------------------------- */

        private void LoadHeaderInfo()
        {
            if (_job == null) return;

            txtPBJobName.Text = _job.JobName;
            txtPBJobNumber.Text = _job.JobNumber.ToString();

            // Job-level date
            var hasPackedPallet = _job.Pallets?.Any(p => p != null /* optionally: && p.WorkOrders?.Any() == true */) == true;

            txtPackDate.Text =
                hasPackedPallet
                    ? _job.EffectivePackDate.ToString("MM/dd/yyyy")
                    : "--/--/----";

        }

        /* -------------------------------------------------------------
         * DASHBOARD
         * ------------------------------------------------------------- */

        private void LoadDashboard(Pallet pallet)
        {
            if (pallet == null)
            {
                txtEnvelopeQty.Text = "0";
                txtScannedWO.Text = "0";
                txtTrayCount.Text = "0";
                txtPackedTime.Text = string.Empty;
                return;
            }

            txtEnvelopeQty.Text = pallet.PalletEnvelopeQty.ToString("N0");
            txtScannedWO.Text = pallet.PalletScannedWO.ToString("N0");
            txtTrayCount.Text = pallet.Trays.ToString("N0");
            txtPackedTime.Text = pallet.PackedTime.ToString("hh:mm tt");
        }

        /* -------------------------------------------------------------
         * REMOVE PALLETS
         * ------------------------------------------------------------- */


        private void btnRemovePallets_Click(object sender, EventArgs e)
        {
            var selected = palletNumListViewList1.GetSelectedIndices();
            if (selected.Count == 0)
            {
                MessageBox.Show("Please select at least one pallet using the checkbox to remove.");
                return;
            }

            using (var dlg = new RemovePallets())
            {
                if (dlg.ShowDialog(this) != DialogResult.Yes)
                    return;
            }


            foreach (var idx in selected.OrderByDescending(i => i))
                _job.Pallets.RemoveAt(idx);

            // ✅ MARK DATA AS CHANGED
            DataChanged = true;

            // Refresh dialog UI
            palletNumListViewList1.RefreshItems(_job);
            LoadDashboard(null);
            palletDetailsListView1.SetItems(null);

        }



        /* -------------------------------------------------------------
         * PALLET CLICK
         * ------------------------------------------------------------- */

        private void OnPalletClicked(int index)
        {
            if (index < 0 || index >= _job.Pallets.Count)
                return;

            var pallet = _job.Pallets[index];
            LoadDashboard(pallet);
            palletDetailsListView1.SetItems(pallet.WorkOrders);
        }

        /* -------------------------------------------------------------
         * FORM BEHAVIOR
         * ------------------------------------------------------------- */

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void ViewButtonDialog_Paint(object sender, PaintEventArgs e)
        {
            CSSDesign.PaintRoundedForm(this, e, _formRadius, Color.Gray);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

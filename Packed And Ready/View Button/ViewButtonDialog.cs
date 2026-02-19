using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class ViewButtonDialog : Form
    {
        /* -------------------------------------------------------------
         * FIELDS & DATA STORAGE
         * ------------------------------------------------------------- */

        List<Bitmap> pages;
        int currentPage = 0;

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

        public ViewButtonDialog(PbJobModel job, bool hideRemove=false , bool hidePrint=false ,bool hideClose=true)
        {
            InitializeComponent();

            _job = job;

            btnRemovePallets.Visible =!hideRemove;
            btnPrintPallets.Visible =! hidePrint;
            btnClose.Visible =! hideClose;
            LoadHeaderInfo();
            lvPallet.SetItems(_job);
            lvPallet.PalletClicked += OnPalletClicked;

            pnlHeader.MouseDown += pnlHeader_MouseDown;
        //    this.Paint += ViewButtonDialog_Paint;

            CSSDesign.MakeRounded(btnRemovePallets, 10);
            CSSDesign.MakeRounded(btnPrintPallets, 10);
            CSSDesign.AddPanelBorder(pnlDashboard, Color.Silver, 1);
            CSSDesign.AddPanelBorder(pnlDetails, Color.Silver, 1);


           
            //onload select first item

        }


        // inside lvPallet UserControl
     

        /* -------------------------------------------------------------
         * HEADER
         * ------------------------------------------------------------- */

        private void LoadHeaderInfo()
        {
            bool isShipped = _job.ShippedDate.HasValue;

            if (isShipped)
            {
                // 🔁 Rename label
                lblPackDate.Text = "Shipped Date and Time";

                // Show shipped date + time
                txtPackDate.Text = _job.ShippedDate.Value.ToString("MM/dd/yyyy  hh:mm tt");
            }
            else
            {
                // Default behavior (Packed)
                lblPackDate.Text = "Packed Date";

                var hasPackedPallet =
                    _job.Pallets?.Any(p => p != null) == true;

                txtPackDate.Text = hasPackedPallet
                    ? _job.EffectivePackDate.ToString("MM/dd/yyyy")
                    : "--/--/----";
            }

        }

        /* -------------------------------------------------------------
         * DASHBOARD
         * ------------------------------------------------------------- */

        private void LoadDashboard(Pallet pallet)
        {
            bool isShipped = _job.ShippedDate.HasValue;

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
            txtTrayCount.Text = pallet.TrayCount.ToString("N0");
            if (isShipped)
            {
                lblPackedTime.Text =
                     "Packed Date Time: " + pallet.PackedTime.Value.ToString("MM/dd/yyyy");

                txtPackedTime.Text = pallet.PackedTime.HasValue
              ? pallet.PackedTime.Value.ToString("hh:mm tt")
              : string.Empty;


            }
            else
            {       txtPackedTime.Text = pallet.PackedTime.HasValue
                ? pallet.PackedTime.Value.ToString("hh:mm tt")
                : string.Empty;

            }
     

        }

        /* -------------------------------------------------------------
         * REMOVE PALLETS
         * ------------------------------------------------------------- */


        private async void btnRemovePallets_Click(object sender, EventArgs e)
        {

            var selectedIndices = lvPallet.GetSelectedIndices();

            if (selectedIndices == null || selectedIndices.Count == 0)
            {
                MessageBox.Show("Please select at least one pallet using the checkbox to remove.");
                return;
            }

            using (var dlg = new RemovePallets())
            {
                if (dlg.ShowDialog(this) != DialogResult.Yes)
                    return;
            }

            try
            {
                // Get pallet IDs from selected rows
                var palletIds = selectedIndices
                    .Select(i => _job.Pallets[i].PalletId)
                    .Where(id => id > 0) // safety
                    .ToList();

                if (!palletIds.Any())
                {
                    MessageBox.Show("Invalid pallet selection.");
                    return;
                }

                // 🔥 Delete from database (workorders + pallets)
                await RqliteClient.DeletePalletsAsync(palletIds);

                // Remove from memory (reverse order to prevent index shift)
                foreach (var index in selectedIndices.OrderByDescending(i => i))
                {
                    _job.Pallets.RemoveAt(index);
                }

                DataChanged = true;

                // Refresh UI
                lvPallet.RefreshItems(_job);
                LoadDashboard(null);
                lvPalletDetails.SetItems(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting pallets:\n\n" + ex.Message);
            }

            //  UpdateButtonsByCounts(model?.Totalpallet, model?.TotalScannedWOOfJob);
        }



        /* -------------------------------------------------------------
         * PALLET CLICK
         * ------------------------------------------------------------- */

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            lvPallet.SelectFirstPallet();
        }
        private void OnPalletClicked(int index)
        {
            if (index < 0 || index >= _job.Pallets.Count)
                return;

            var pallet = _job.Pallets[index];
            LoadDashboard(pallet);
            lvPalletDetails.SetItems(pallet.WorkOrders);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
             if (pages == null || currentPage >= pages.Count)
            {
                e.HasMorePages = false;
                currentPage = 0;
                return;
            }

            Bitmap page = pages[currentPage];

            // Fit width to page margin
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            int w = e.MarginBounds.Width;
            int h = page.Height * w / page.Width;

            e.Graphics.DrawImage(page, new Rectangle(x, y, w, h));

            currentPage++;

            e.HasMorePages = currentPage < pages.Count;
        }
        /* -------------------------------------------------------------
         * PRINTING
         * ------------------------------------------------------------- */


        private void btnPrintPallets_Click(object sender, EventArgs e)
        {


            // Step 1: Capture full scrollable content
            Bitmap full = CaptureScrollablePanel(pnlDashboard);

            // Step 2: Convert to pages
            SliceIntoPages(full, 1120); // Adjust page height if needed

            // Step 3: Setup PrintDocument
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += printDocument1_PrintPage;

            // Step 4: Show Preview
            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = doc;
            preview.Width = 1200;
            preview.Height = 800;

            preview.ShowDialog();   // <- This shows the preview BEFORE printing


        }

        private Bitmap CaptureScrollablePanel(Panel panel)
        {
            // Save original size
            int originalHeight = panel.Height;

            // Expand the panel to full scroll height
            panel.AutoScroll = false;
            panel.Height = panel.DisplayRectangle.Height;

            // Create a bitmap based on full height
            Bitmap bmp = new Bitmap(panel.Width, panel.Height);
            panel.DrawToBitmap(bmp, new Rectangle(0, 0, panel.Width, panel.Height));

            // Restore original
            panel.Height = originalHeight;
            panel.AutoScroll = true;

            return bmp;
        }

        private void SliceIntoPages(Bitmap fullImage, int pageHeight)
        {
            pages = new List<Bitmap>();
            int y = 0;

            while (y < fullImage.Height)
            {
                int sliceHeight = Math.Min(pageHeight, fullImage.Height - y);

                Bitmap page = new Bitmap(fullImage.Width, sliceHeight);

                using (Graphics g = Graphics.FromImage(page))
                {
                    g.DrawImage(fullImage, new Rectangle(0, 0, page.Width, page.Height),
                        new Rectangle(0, y, page.Width, sliceHeight), GraphicsUnit.Pixel);
                }

                pages.Add(page);
                y += sliceHeight;
            }

        }


    }
}

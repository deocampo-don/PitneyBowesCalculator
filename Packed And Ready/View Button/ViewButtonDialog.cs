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

        public ViewButtonDialog(PbJobModel job, bool hideRemove = false, bool hidePrint = false, bool hideClose = true)
        {
            InitializeComponent();

            _job = job;

            btnRemovePallets.Visible = !hideRemove;
            btnPrintPallets.Visible = !hidePrint;
            btnClose.Visible = !hideClose;
            LoadHeaderInfo();
            lvPallet.SetItems(_job);
            lvPallet.PalletClicked += OnPalletClicked;

            pnlHeader.MouseDown += pnlHeader_MouseDown;
            //    this.Paint += ViewButtonDialog_Paint;

            CSSDesign.MakeRounded(btnRemovePallets, 10);
            CSSDesign.MakeRounded(btnPrintPallets, 10);
            CSSDesign.AddPanelBorder(pnlDashboard, Color.Silver, 1);
            CSSDesign.AddPanelBorder(pnlDetails, Color.Silver, 1);
            CSSDesign.AddPanelBorder(pnlPalletNum, Color.Silver, 1);




            //onload select first item

        }


        // inside lvPallet UserControl


        /* -------------------------------------------------------------
         * HEADER
         * ------------------------------------------------------------- */

        private void LoadHeaderInfo()
        {
            txtPBJobName.Text = _job.JobName ?? string.Empty;
            txtPBJobNumber.Text = _job.JobNumber.ToString();
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
            if (pallet == null)
            {
                txtEnvelopeQty.Text = "0";
                txtScannedWO.Text = "0";
                txtTrayCount.Text = "0";
                txtPackedTime.Text = string.Empty;
                return;
            }

            bool isShipped = pallet.State == PalletState.Shipped;

            txtEnvelopeQty.Text = pallet.PalletEnvelopeQty.ToString("N0");
            txtScannedWO.Text = pallet.PalletScannedWO.ToString("N0");
            txtTrayCount.Text = pallet.TrayCount.ToString("N0");

            if (isShipped)
            {
                lblPackedTime.Text =
                    "Packed Date Time: " +
                    (pallet.PackedAt?.ToString("MM/dd/yyyy") ?? "--/--/----");

                txtPackedTime.Text = pallet.PackedAt.HasValue
                    ? pallet.PackedAt.Value.ToString("hh:mm tt")
                    : string.Empty;
            }
            else
            {
                txtPackedTime.Text = pallet.PackedAt.HasValue
                    ? pallet.PackedAt.Value.ToString("hh:mm tt")
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
                MessageBox.Show("Please select at least one pallet using the checkbox.");
                return;
            }

            var selectedPallets = selectedIndices
                .Select(i => _job.Pallets[i])
                .ToList();

            var selectedPacked = selectedPallets
                .Where(p => p.State == PalletState.Packed)
                .ToList();

            var selectedUnpacked = selectedPallets
                .Where(p => p.State == PalletState.NotReady)
                .ToList();

            var ongoing = _job.Pallets
                .FirstOrDefault(p => p.State == PalletState.NotReady);

            using (var dlg = new RemovePallets(ongoing != null))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    // =======================
                    // MERGE
                    // =======================
                    if (dlg.Action == RemovePallets.RemoveAction.Merge)
                    {
                        if (ongoing == null)
                        {
                            MessageBox.Show("No active pallet available to merge into.");
                            return;
                        }

                        if (selectedUnpacked.Any())
                        {
                            MessageBox.Show(
                                "You cannot merge because the ongoing pallet is also selected.\n\nPlease uncheck the unpacked pallet.",
                                "Invalid Merge",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }

                        var sourceIds = selectedPacked
                            .Select(p => p.PalletId)
                            .ToList();

                        if (!sourceIds.Any())
                        {
                            MessageBox.Show("Only packed pallets can be merged.");
                            return;
                        }

                        await RqliteClient.MergePalletsIntoAsync(
                            sourceIds,
                            ongoing.PalletId);

                        DataChanged = true;
                        Close();
                        return;
                    }

                    // =======================
                    // DELETE
                    // =======================
                    if (dlg.Action == RemovePallets.RemoveAction.Delete)
                    {
                        var palletIds = selectedPallets
                            .Select(p => p.PalletId)
                            .Where(id => id > 0)
                            .ToList();

                        if (!palletIds.Any())
                        {
                            MessageBox.Show("Invalid pallet selection.");
                            return;
                        }

                        await RqliteClient.DeletePalletsAsync(palletIds);

                        DataChanged = true;
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error processing pallets:\n\n" + ex.Message);
                }
            }
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

    
   

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /* -------------------------------------------------------------
      * PRINTING
      * ------------------------------------------------------------- */

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (pages == null || currentPage >= pages.Count)
            {
                e.HasMorePages = false;
                currentPage = 0;
                return;
            }

            Bitmap img = pages[currentPage];

            // Fit-to-page (preserve aspect) using min scale
            float scale = Math.Min(
                (float)e.MarginBounds.Width / img.Width,
                (float)e.MarginBounds.Height / img.Height
            );

            int drawW = (int)(img.Width * scale);
            int drawH = (int)(img.Height * scale);

            // Optional: center horizontally
            int drawX = e.MarginBounds.Left + (e.MarginBounds.Width - drawW) / 2;
            int drawY = e.MarginBounds.Top;

            e.Graphics.DrawImage(img, new Rectangle(drawX, drawY, drawW, drawH));

            currentPage++;
            e.HasMorePages = currentPage < pages.Count;
        }



        private void btnPrintPallets_Click(object sender, EventArgs e)
        {
            Bitmap bmpDashboard = CaptureControl(pnlDashboard); // header only
            Bitmap bmpDetails = CaptureScrollablePanel(lvPalletDetails.ScrollContainer);

            Bitmap full = CombineVertical(bmpDashboard, bmpDetails);

            // Slice into reasonable page chunks (pixel units of the bitmap)
            // 2200–2600 works well for portrait at typical printer DPI when later fit-to-page.
            SliceIntoPages(full, 2400);

            PrintDocument doc = new PrintDocument();
            doc.PrintPage += printDocument1_PrintPage;

            using (var preview = new PrintPreviewDialog
            {
                Document = doc,
                Width = 1200,
                Height = 800
            })
            {
                preview.ShowDialog(this);
            }
        }


        Bitmap CaptureControl(Control ctrl)
        {
            Bitmap bmp = new Bitmap(ctrl.Width, ctrl.Height);
            ctrl.DrawToBitmap(bmp, new Rectangle(0, 0, ctrl.Width, ctrl.Height));
            return bmp;
        }



        Bitmap CombineVertical(Bitmap top, Bitmap bottom)
        {
            Bitmap final = new Bitmap(
                Math.Max(top.Width, bottom.Width),
                top.Height + bottom.Height
            );

            using (Graphics g = Graphics.FromImage(final))
            {
                g.DrawImage(top, 0, 0);
                g.DrawImage(bottom, 0, top.Height);
            }

            return final;
        }

        Bitmap CaptureScrollablePanel(FlowLayoutPanel flp)
        {
            // Ensure layout is up-to-date
            flp.PerformLayout();
            flp.Refresh();

            int originalHeight = flp.Height;
            bool originalAutoScroll = flp.AutoScroll;

            try
            {
                // Disable scrolling so DrawToBitmap can "see" everything
                flp.AutoScroll = false;

                // Compute the real full content height (bottom-most control)
                int fullHeight = 0;
                foreach (Control c in flp.Controls)
                {
                    int bottom = c.Bottom + c.Margin.Bottom;
                    if (bottom > fullHeight)
                        fullHeight = bottom;
                }
                fullHeight += flp.Padding.Bottom;

                // Expand temporarily to show all rows
                if (fullHeight < flp.Height)
                    fullHeight = flp.Height; // safety
                flp.Height = fullHeight;

                int fullWidth = Math.Max(flp.DisplayRectangle.Width, flp.Width);

                Bitmap bmp = new Bitmap(fullWidth, fullHeight);
                flp.DrawToBitmap(bmp, new Rectangle(0, 0, fullWidth, fullHeight));
                return bmp;
            }
            finally
            {
                // Restore original state
                flp.Height = originalHeight;
                flp.AutoScroll = originalAutoScroll;
                flp.PerformLayout();
                flp.Refresh();
            }
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

using Jds2;
using Jds2.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class ViewButtonDialog : Form
    {
        /* -------------------------------------------------------------
         * FIELDS & DATA STORAGE
         * ------------------------------------------------------------- */

        private PbJobModel _job;
        private List<Pallet> palletsToPrint;
        private int palletPrintIndex = 0;
        private int workOrderIndex = 0;
        private int currentPage = 1;
        private int totalPages = 1;

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

            btnRemovePallet.Visible = !hideRemove;
            btnPrintPallet.Visible = !hidePrint;

            LoadHeaderInfo();
            lvPallet.SetItems(_job);
            lvPallet.PalletClicked += OnPalletClicked;

            pnlHeader.MouseDown += pnlHeader_MouseDown;



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


        /* -------------------------------------------------------------
      * PRINTING
      * ------------------------------------------------------------- */


        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (palletsToPrint == null || palletPrintIndex >= palletsToPrint.Count)
            {
                e.HasMorePages = false;
                return;
            }

            Pallet pallet = palletsToPrint[palletPrintIndex];

            int left = 60;
            int colWorkOrder = left;
            int colQty = 650;

            int y = 50;
            int bottomLimit = e.MarginBounds.Bottom - 60;

            Font headerFont = new Font("Segoe UI", 18, FontStyle.Bold);
            Font palletFont = new Font("Segoe UI", 14, FontStyle.Bold);
            Font labelFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Font textFont = new Font("Segoe UI", 10);
            Font footerFont = new Font("Segoe UI", 9);

            Pen linePen = new Pen(Color.Black, 1);

            /* ---------- HEADER ---------- */

            e.Graphics.DrawString($"PB JOB {_job.JobNumber}", headerFont, Brushes.Black, left, y);
            y += 45;

            e.Graphics.DrawString($"PALLET #{palletPrintIndex + 1}", palletFont, Brushes.Black, left, y);
            y += 30;

            e.Graphics.DrawString("Printed:", labelFont, Brushes.Black, left, y);
            e.Graphics.DrawString(DateTime.Now.ToString("MM/dd/yyyy  hh:mm tt"), textFont, Brushes.Black, left + 90, y);

            e.Graphics.DrawString("Packed Date:", labelFont, Brushes.Black, 420, y);
            e.Graphics.DrawString(
                pallet.PackedAt?.ToString("MM/dd/yyyy  hh:mm tt") ?? "--",
                textFont,
                Brushes.Black,
                540,
                y
            );

            y += 30;

            e.Graphics.DrawLine(linePen, left, y, 700, y);
            y += 25;

            /* ---------- PALLET SUMMARY ---------- */

            e.Graphics.DrawString("Envelope Qty :", labelFont, Brushes.Black, left, y);
            e.Graphics.DrawString(pallet.PalletEnvelopeQty.ToString(), textFont, Brushes.Black, left + 140, y);

            e.Graphics.DrawString("Tray Count :", labelFont, Brushes.Black, 420, y);
            e.Graphics.DrawString(pallet.TrayCount.ToString(), textFont, Brushes.Black, 540, y);

            y += 25;

            e.Graphics.DrawString("Scanned WO :", labelFont, Brushes.Black, left, y);
            e.Graphics.DrawString(pallet.PalletScannedWO.ToString(), textFont, Brushes.Black, left + 140, y);

            e.Graphics.DrawString("Packed Time :", labelFont, Brushes.Black, 420, y);
            e.Graphics.DrawString(pallet.PackedAt?.ToString("hh:mm tt") ?? "", textFont, Brushes.Black, 540, y);

            y += 40;

            /* ---------- WORK ORDER HEADER ---------- */

            e.Graphics.DrawString("WORK ORDERS", palletFont, Brushes.Black, left, y);
            y += 30;

            e.Graphics.DrawLine(linePen, left, y, 700, y);
            y += 20;

            // Column headers
            e.Graphics.DrawString("WORK ORDER", labelFont, Brushes.Black, colWorkOrder, y);
            e.Graphics.DrawString("QTY", labelFont, Brushes.Black, colQty, y);

            y += 20;

            e.Graphics.DrawLine(linePen, left, y, 700, y);
            y += 20;

            /* ---------- WORK ORDERS ---------- */

            while (workOrderIndex < pallet.WorkOrders.Count)
            {
                var wo = pallet.WorkOrders[workOrderIndex];

                if (y > bottomLimit)
                {
                    DrawFooter(e);
                    e.HasMorePages = true;
                    return;
                }

                e.Graphics.DrawString(wo.WorkOrderCode, textFont, Brushes.Black, colWorkOrder, y);
                e.Graphics.DrawString(wo.Quantity.ToString(), textFont, Brushes.Black, colQty, y);

                y += 22;
                workOrderIndex++;
            }

            /* ---------- FOOTER ---------- */

            DrawFooter(e);

            workOrderIndex = 0;
            palletPrintIndex++;

            e.HasMorePages = palletPrintIndex < palletsToPrint.Count;
        }

        private void DrawFooter(PrintPageEventArgs e)
        {
            Font footerFont = new Font("Segoe UI", 9);

            int footerLineY = e.MarginBounds.Bottom;

            e.Graphics.DrawLine(Pens.Gray, e.MarginBounds.Left, footerLineY, e.MarginBounds.Right, footerLineY);

            string pageText = $"Page {palletPrintIndex + 1} of {palletsToPrint.Count}";
            SizeF size = e.Graphics.MeasureString(pageText, footerFont);

            float x = e.MarginBounds.Right - size.Width;
            float y = footerLineY + 5;

            e.Graphics.DrawString(pageText, footerFont, Brushes.Black, x, y);
        }

        private async void btnRemovePallet_Click_1(object sender, EventArgs e)
        {

            var selectedIndices = lvPallet.GetSelectedIndices();

            if (selectedIndices == null || selectedIndices.Count == 0)
            {
                Debug.WriteLine("No pallets selected.");
                MessageBox.Show("Please select at least one pallet.");
                return;
            }

            var selectedPallets = selectedIndices
                .Select(i => _job.Pallets[i])
                .ToList();

            if (!selectedPallets.All(p => p.State == PalletState.Ready))
            {

                MessageBox.Show(
                    "Only packed pallets (Ready) can be removed.",
                    "Invalid Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var palletIds = selectedPallets
                .Select(p => p.PalletId)
                .Where(id => id > 0)
                .ToList();

            Debug.WriteLine("PalletIds selected: " + string.Join(",", palletIds));

            if (!palletIds.Any())
            {

                MessageBox.Show("Invalid pallet selection.");
                return;
            }

            try
            {

                var activePalletId = await RqliteClient.GetActivePalletIdAsync(_job.JobId);
                bool hasActivePallet = activePalletId != null;

                using (var dlg = new RemovePallets(hasActivePallet))
                {
                    if (dlg.ShowDialog(this) != DialogResult.OK)
                    {

                        return;
                    }

                    switch (dlg.Action)
                    {
                        case RemovePallets.RemoveAction.Merge:
                            if (activePalletId == null)
                            {

                                MessageBox.Show("Active pallet no longer exists. Please refresh.");
                                return;
                            }
                            await RqliteClient.MergePalletsIntoAsync(
                                palletIds,
                                activePalletId.Value);
                            break;

                        case RemovePallets.RemoveAction.Delete:
                            await RqliteClient.DeletePalletsAsync(palletIds);

                            break;
                        case RemovePallets.RemoveAction.UndoPack:

                            // If there is no active pallet, only one pallet can be unpacked
                            if (!hasActivePallet && palletIds.Count > 1)
                            {
                                MessageBox.Show(
                                    "When there is no active pallet, only one pallet can be unpacked.",
                                    "Invalid Operation",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                return;
                            }

                            await RqliteClient.UndoPackedPalletAsync(
                                palletIds,
                                _job.JobId);
                            break;

                        default:

                            return;
                    }
                }


                DataChanged = true;


                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error processing pallets:\n\n" + ex.Message);
            }
        }

        private async void btnPrintPallet_Click_1(object sender, EventArgs e)
        {
            btnPrintPallet.Enabled = false;

            Utils.showStatusAndSpinner(lbStatus, pbSpinner, "Attempting to print...");

            try
            {
                await PrintSelectedPalletsAsync();
            }
            finally
            {
                //Utils.hideStatusAndSpinner(lbStatus, pbSpinner, "Attempting to print...");
                btnPrintPallet.Enabled = true;
            }
        }


        private void SaveReportAsPdf(string filePath)
        {
            palletPrintIndex = 0;
            workOrderIndex = 0;

            PrintDocument doc = new PrintDocument();

            doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            doc.PrinterSettings.PrintToFile = true;
            doc.PrinterSettings.PrintFileName = filePath;

            doc.PrintPage += PrintDocument_PrintPage;

            doc.Print();
        }

        private void PrintPdf(string pdfPath)
        {
            var printerName = Program.AppINI._defaultPrinter;
            var printerIp = Program.AppINI._printerIP;
            var printerPort = Program.AppINI._printerPort;

            if (!File.Exists(pdfPath))
            {
                MessageBox.Show("PDF file not found.");
                return;
            }

            /* -------------------------------------------------------------
               TRY NETWORK PRINTER FIRST
            ------------------------------------------------------------- */

            if (!string.IsNullOrWhiteSpace(printerIp) &&
                !string.IsNullOrWhiteSpace(printerPort) &&
                int.TryParse(printerPort, out int port))
            {
                try
                {
                    PrintPdfToNetworkPrinter(pdfPath, printerIp, port);
                    return;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Network printer failed: " + ex.Message);
                }
            }

            /* -------------------------------------------------------------
               FALLBACK TO WINDOWS DEFAULT PRINTER
            ------------------------------------------------------------- */

            if (!string.IsNullOrWhiteSpace(printerName))
            {
                bool printerExists = PrinterSettings.InstalledPrinters
                    .Cast<string>()
                    .Any(p => p.Equals(printerName, StringComparison.OrdinalIgnoreCase));

                if (printerExists)
                {
                    try
                    {
                        PrintPdfToDefaultPrinter(pdfPath);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Default printer failed: " + ex.Message);
                    }
                }
            }

            /* -------------------------------------------------------------
               NO PRINTER AVAILABLE
            ------------------------------------------------------------- */

            MessageBox.Show(
     "Unable to print.\n\n" +
     "No reachable network printer and no default printer configured. Configure printer in settings first!",
     "Printing Error",
     MessageBoxButtons.OK,
     MessageBoxIcon.Warning);
            Utils.errorStatusAndSpinner(lbStatus, pbSpinner, "Printer failed!");
        }
        private void PrintPdfToDefaultPrinter(string pdfPath)
        {
            var printerName = Program.AppINI._defaultPrinter;

            if (string.IsNullOrWhiteSpace(printerName))
            {
                MessageBox.Show("No default printer configured.");
                return;
            }

            if (!File.Exists(pdfPath))
            {
                MessageBox.Show("PDF file not found.");
                return;
            }

            bool printerExists = PrinterSettings.InstalledPrinters
                .Cast<string>()
                .Any(p => p.Equals(printerName, StringComparison.OrdinalIgnoreCase));

            if (!printerExists)
            {
                MessageBox.Show($"Printer '{printerName}' is not installed.");
                return;
            }

            try
            {
                var printer = new Jds2.SimpleFreePdfPrinter();
                printer.PrintPdfTo(printerName, pdfPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Printing failed:\n{ex.Message}");
            }
        }

        private void PrintPdfToNetworkPrinter(string pdfPath, string printerIp, int printerPort)
        {
            if (!File.Exists(pdfPath))
                throw new Exception("PDF file not found.");

            byte[] fileBytes = File.ReadAllBytes(pdfPath);

            using (TcpClient client = new TcpClient())
            {
                var result = client.BeginConnect(printerIp, printerPort, null, null);

                bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5));

                if (!success)
                    throw new Exception("Printer connection timeout.");

                client.EndConnect(result);

                using (NetworkStream stream = client.GetStream())
                {
                    stream.Write(fileBytes, 0, fileBytes.Length);
                    stream.Flush();
                }
            }
        }
        private async Task PrintSelectedPalletsAsync()
        {
            var selectedIndices = lvPallet.GetSelectedIndices();

            if (selectedIndices == null || selectedIndices.Count == 0)
            {
                MessageBox.Show("Select pallet(s) to print.");
                return;
            }

            palletsToPrint = selectedIndices
                .Select(i => _job.Pallets[i])
                .ToList();

            string pdfPath = Path.Combine(
                Path.GetTempPath(),
                $"PBJob_{_job.JobNumber}_{DateTime.Now:yyyyMMddHHmmss}.pdf"
            );

            lbStatus.Text = "Generating PDF...";

            try
            {
                await Task.Run(() =>
                {
                    SaveReportAsPdf(pdfPath);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to generate PDF:\n\n" + ex.Message);
                return;
            }

            /* -------------------------------------------------------------
               WAIT FOR PDF DRIVER TO FINISH WRITING
            ------------------------------------------------------------- */

            int wait = 0;
            while (!File.Exists(pdfPath) && wait < 5000)
            {
                await Task.Delay(100);
                wait += 100;
            }

            if (!File.Exists(pdfPath))
            {
                MessageBox.Show("PDF was not generated.");
                return;
            }

            /* -------------------------------------------------------------
               OPEN PDF FOR USER VIEWING
            ------------------------------------------------------------- */

            Process.Start(new ProcessStartInfo
            {
                FileName = pdfPath,
                UseShellExecute = true
            });

            lbStatus.Text = "Sending to printer...";

            try
            {
                await Task.Run(() =>
                {
                    PrintPdf(pdfPath);
                });

                //lbStatus.Text = "Printed successfully";
            }
            catch (Exception ex)
            {
                Utils.errorStatusAndSpinner(lbStatus, pbSpinner, "Printer not available!");
                

                MessageBox.Show(
                    "Unable to print.\n\n" + ex.Message,
                    "Printing Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}


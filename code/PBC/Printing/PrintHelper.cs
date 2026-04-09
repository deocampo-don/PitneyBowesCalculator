using Jds2;
using PitneyBowesCalculator;
using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Windows;

public static class PrintHelper
{
    public static void PrintPdf(string pdfPath)
    {
        var printerName = Program.AppINI._defaultPrinter;
        var printerIp = Program.AppINI._printerIP;
        var printerPort = Program.AppINI._printerPort;

        if (!File.Exists(pdfPath))
            throw new Exception("PDF file not found.");

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
                Utils.WriteExceptionError(ex);
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
                    var printer = new SimpleFreePdfPrinter();
                    printer.PrintPdfTo(printerName, pdfPath);
                    return;
                }
                catch (Exception ex)
                {
                    Utils.WriteExceptionError (ex);
                }
            }
        }

        /* -------------------------------------------------------------
           NO PRINTER AVAILABLE
        ------------------------------------------------------------- */
        //throw new Exception(
        //    "No reachable network printer and no default printer configured.\n\n" +
        //    "Configure printer in settings first!"
        //);
        MessageDialogBox.ShowDialog("Notice", "No reachable network printer and no default printer configured.\n\n" +
            "Configure printer in settings first!", System.Windows.Forms.MessageBoxButtons.OK, MessageType.Error);
    }

    private static void PrintPdfToNetworkPrinter(string pdfPath, string ip, int port)
    {
        byte[] fileBytes = File.ReadAllBytes(pdfPath);

        using (TcpClient client = new TcpClient())
        {
            var result = client.BeginConnect(ip, port, null, null);
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
}
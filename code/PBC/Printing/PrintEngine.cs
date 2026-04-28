using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;

public static class PrintEngine
{
    public static void Print(Action<PrintPageEventArgs> drawAction)
    {
        string pdfPath = Path.Combine(
            Path.GetTempPath(),
            $"Print_{DateTime.Now:yyyyMMddHHmmss}.pdf"
        );

        PrintDocument doc = new PrintDocument();

        doc.PrintPage += (s, e) =>
        {
            drawAction(e);
            e.HasMorePages = false;
        };

        doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";
        doc.PrinterSettings.PrintToFile = true;
        doc.PrinterSettings.PrintFileName = pdfPath;

        doc.Print();

        Process.Start(new ProcessStartInfo
        {
            FileName = pdfPath,
            UseShellExecute = true
        });

        PrintHelper.PrintPdf(pdfPath); // your existing logic
    }

    public static void PrintMultiPage(Action<PrintPageEventArgs> drawAction)
    {
        string pdfPath = Path.Combine(
            Path.GetTempPath(),
            $"Print_{DateTime.Now:yyyyMMddHHmmss}.pdf"
        );

        PrintDocument doc = new PrintDocument();

        doc.PrintPage += (s, e) =>
        {
            drawAction(e); // caller controls e.HasMorePages
        };

        doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";
        doc.PrinterSettings.PrintToFile = true;
        doc.PrinterSettings.PrintFileName = pdfPath;

        doc.Print();

        Process.Start(new ProcessStartInfo
        {
            FileName = pdfPath,
            UseShellExecute = true
        });

        PrintHelper.PrintPdf(pdfPath);
    }
}
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;

public static class PrintLayouts
{
    public static void DrawSummary(PrintPageEventArgs e, List<PbJobModel> jobs)
    {
        int left = 60;
        int y = 50;

        Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
        Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
        Font textFont = new Font("Segoe UI", 10);

        int rowHeight = 25;

        int colJob = left;
        int colQty = 400;
        int colTrays = 480;
        int colPallets = 560;
        int colShip = 650;

        // Title
        e.Graphics.DrawString("Pitney Bose Summary Sheet", titleFont, Brushes.Black, left, y);
        y += 40;

        e.Graphics.DrawString("Timestamp: " + DateTime.Now.ToString("M/d/yy h:mmtt"),
            textFont, Brushes.Black, left, y);

        y += 40;

        // Header
        e.Graphics.FillRectangle(Brushes.LightGray, left, y, 750, rowHeight);

        e.Graphics.DrawString("Job ID", headerFont, Brushes.Black, colJob, y + 5);
        e.Graphics.DrawString("QTY", headerFont, Brushes.Black, colQty, y + 5);
        e.Graphics.DrawString("TRAYS", headerFont, Brushes.Black, colTrays, y + 5);
        e.Graphics.DrawString("PALLETS", headerFont, Brushes.Black, colPallets, y + 5);
        e.Graphics.DrawString("SHIP DATE", headerFont, Brushes.Black, colShip, y + 5);

        y += rowHeight;

        bool alt = false;

        foreach (var job in jobs)
        {
            if (alt)
                e.Graphics.FillRectangle(Brushes.Gainsboro, left, y, 750, rowHeight);

            e.Graphics.DrawString($"{job.JobNumber} {job.JobName}", textFont, Brushes.Black, colJob, y + 5);
            e.Graphics.DrawString(job.TotalEnvelopeOfJob.ToString(), textFont, Brushes.Black, colQty, y + 5);
            e.Graphics.DrawString(job.TotalTraysOfJob.ToString(), textFont, Brushes.Black, colTrays, y + 5);
            e.Graphics.DrawString(job.Pallets.Count.ToString(), textFont, Brushes.Black, colPallets, y + 5);
            e.Graphics.DrawString(job.ShippedDate?.ToString("MM/dd/yyyy hh:mm tt") ?? "--",
                textFont, Brushes.Black, colShip, y + 5);

            y += rowHeight;
            alt = !alt;
        }
    }

    public static void SummaryShip(PrintPageEventArgs e, List<PbJobModel> jobs)
    {
        int left = 60;
        int y = 50;

        Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
        Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
        Font textFont = new Font("Segoe UI", 10);

        int rowHeight = 25;

        int colJob = left;
        int colQty = 400;
        int colTrays = 480;
        int colPallets = 560;
        int colShip = 650;

        // Title
        e.Graphics.DrawString("Pitney Bose Summary Sheet", titleFont, Brushes.Black, left, y);
        y += 40;

        e.Graphics.DrawString("Timestamp: " + DateTime.Now.ToString("M/d/yy h:mmtt"),
            textFont, Brushes.Black, left, y);

        y += 40;

        // Header
        e.Graphics.FillRectangle(Brushes.LightGray, left, y, 750, rowHeight);

        e.Graphics.DrawString("Job ID", headerFont, Brushes.Black, colJob, y + 5);
        e.Graphics.DrawString("QTY", headerFont, Brushes.Black, colQty, y + 5);
        e.Graphics.DrawString("TRAYS", headerFont, Brushes.Black, colTrays, y + 5);
        e.Graphics.DrawString("PALLETS", headerFont, Brushes.Black, colPallets, y + 5);
        e.Graphics.DrawString("PACK DATE", headerFont, Brushes.Black, colShip, y + 5);

        y += rowHeight;

        bool alt = false;

        foreach (var job in jobs)
        {
            if (alt)
                e.Graphics.FillRectangle(Brushes.Gainsboro, left, y, 750, rowHeight);

            e.Graphics.DrawString($"{job.JobNumber} {job.JobName}", textFont, Brushes.Black, colJob, y + 5);
            e.Graphics.DrawString(job.TotalEnvelopeOfJob.ToString(), textFont, Brushes.Black, colQty, y + 5);
            e.Graphics.DrawString(job.TotalTraysOfJob.ToString(), textFont, Brushes.Black, colTrays, y + 5);
            e.Graphics.DrawString(job.Pallets.Count.ToString(), textFont, Brushes.Black, colPallets, y + 5);
            e.Graphics.DrawString(job.LastPackedTime?.ToString("MM/dd/yyyy hh:mm tt") ?? "--",
    textFont, Brushes.Black, colShip, y + 5);

            y += rowHeight;
            alt = !alt;
        }
    }

    public static void DrawPallets(PrintPageEventArgs e, PbJobModel job, List<Pallet> pallets)
    {
        int left = 60;
        int y = 50;
        int rowHeight = 28;

        Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
        Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
        Font textFont = new Font("Segoe UI", 10);

        // Column widths: Pallet | Qty | WOs | Trays | Pack Time
        int[] colWidths = { 150, 100, 150, 100, 200 };

        int[] colX = new int[colWidths.Length];
        colX[0] = left;
        for (int i = 1; i < colWidths.Length; i++)
        {
            colX[i] = colX[i - 1] + colWidths[i - 1];
        }

        // Alignment
        StringFormat leftAlign = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };

        StringFormat centerAlign = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        StringFormat rightAlign = new StringFormat
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Center
        };

        // TITLE
        e.Graphics.DrawString(
            $"Pallet Report - {job.JobNumber} {job.JobName}",
            titleFont,
            Brushes.Black,
            left,
            y
        );

        y += 40;

        // TIMESTAMP
        e.Graphics.DrawString(
            "Timestamp: " + DateTime.Now.ToString("M/d/yy h:mmtt"),
            textFont,
            Brushes.Black,
            left,
            y
        );

        y += 40;

        // HEADER
        string[] headers = { "Pallet #", "QTY", "Scanned WOs", "TRAYS", "PACK TIME" };

        for (int i = 0; i < headers.Length; i++)
        {
            Rectangle rect = new Rectangle(colX[i], y, colWidths[i], rowHeight);

            e.Graphics.FillRectangle(Brushes.LightGray, rect);
            e.Graphics.DrawRectangle(Pens.Gray, rect);

            var format = (i == 0) ? leftAlign : centerAlign;

            e.Graphics.DrawString(headers[i], headerFont, Brushes.Black, rect, format);
        }

        y += rowHeight;

        // ROWS
        bool alternate = false;
        int index = 1; // ✅ FIX: start at 1

        foreach (var p in pallets)
        {
            for (int i = 0; i < colWidths.Length; i++)
            {
                Rectangle rect = new Rectangle(colX[i], y, colWidths[i], rowHeight);

                if (alternate)
                    e.Graphics.FillRectangle(Brushes.Gainsboro, rect);

                e.Graphics.DrawRectangle(Pens.LightGray, rect);

                string text = i switch
                {
                    0 => index.ToString(), // ✅ FIXED: use index instead of p.PalletNumber
                    1 => p.PalletEnvelopeQty.ToString("N0"),
                    2 => p.PalletScannedWO.ToString(),
                    3 => p.TrayCount.ToString("N0"),
                    4 => p.PackedAt?.ToString("MM/dd/yyyy hh:mm tt") ?? "--",
                    _ => ""
                };

                var format = i switch
                {
                    0 => leftAlign,
                    1 => rightAlign,
                    2 => centerAlign,
                    3 => rightAlign,
                    _ => centerAlign
                };

                // Padding
                Rectangle padded = new Rectangle(rect.X + 5, rect.Y, rect.Width - 10, rect.Height);

                e.Graphics.DrawString(text, textFont, Brushes.Black, padded, format);
            }

            y += rowHeight;
            alternate = !alternate;
            index++; // ✅ increment
        }
    
}
}
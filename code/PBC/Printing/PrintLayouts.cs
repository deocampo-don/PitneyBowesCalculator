using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;

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

        e.Graphics.DrawString("Pitney Bose Summary Sheet", titleFont, Brushes.Black, left, y);
        y += 40;

        e.Graphics.DrawString("Timestamp: " + DateTime.Now.ToString("M/d/yy h:mmtt"),
            textFont, Brushes.Black, left, y);
        y += 40;

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
            var latestShipTime = job.Pallets
                .Where(p => p.State == PalletState.Shipped && p.ShippedAt.HasValue)
                .Max(p => p.ShippedAt);

            var shippedPallets = job.Pallets
                .Where(p => p.State == PalletState.Shipped && p.ShippedAt == latestShipTime)
                .ToList();

            int totalEnvelopes = shippedPallets.Sum(p => p.PalletEnvelopeQty);
            int totalTrays = shippedPallets.Sum(p => p.TrayCount);
            int palletCount = shippedPallets.Count;

            // ✅ Fix — fall back to DateTime.Now if ShippedAt not yet committed to DB
            var shipDate = shippedPallets
                .Where(p => p.ShippedAt.HasValue)
                .Max(p => p.ShippedAt) ?? DateTime.Now;

            if (alt)
                e.Graphics.FillRectangle(Brushes.Gainsboro, left, y, 750, rowHeight);

            e.Graphics.DrawString($"{job.JobNumber} {job.JobName}", textFont, Brushes.Black, colJob, y + 5);
            e.Graphics.DrawString(totalEnvelopes.ToString(), textFont, Brushes.Black, colQty, y + 5);
            e.Graphics.DrawString(totalTrays.ToString(), textFont, Brushes.Black, colTrays, y + 5);
            e.Graphics.DrawString(palletCount.ToString(), textFont, Brushes.Black, colPallets, y + 5);
            e.Graphics.DrawString(shipDate.ToString("MM/dd/yyyy hh:mm tt"),
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
    public static void DrawPalletLabel(PrintPageEventArgs e, PbJobModel job, Pallet pallet, int palletIndex)
    {
        int leftMargin = 80;
        int y = 80;

        int pageWidth = e.PageBounds.Width;

        Font jobFont = new Font("Segoe UI", 24, FontStyle.Regular);
        Font palletFont = new Font("Segoe UI", 42, FontStyle.Bold);
        Font labelFont = new Font("Segoe UI", 16, FontStyle.Regular);

        StringFormat centerFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Near
        };

        // --- Job Number + Name (centered) ---
        string jobTitle = $"{job.JobNumber} {job.JobName}";
        RectangleF jobRect = new RectangleF(0, y, pageWidth, 50);
        e.Graphics.DrawString(jobTitle, jobFont, Brushes.Black, jobRect, centerFormat);

        y += 80;

        // --- Pallet # X (big and bold, centered) ---
        string palletTitle = $"Pallet # {palletIndex}";
   
        RectangleF palletRect = new RectangleF(0, y, pageWidth, 80);
        e.Graphics.DrawString(palletTitle, palletFont, Brushes.Black, palletRect, centerFormat);

        y += 120;

        // --- Details (left aligned, spaced out like label) ---
        int lineSpacing = 45;

        e.Graphics.DrawString($"Envelope QTY: {pallet.PalletEnvelopeQty:N0}", labelFont, Brushes.Black, leftMargin, y);
        y += lineSpacing;

        e.Graphics.DrawString($"Tray: {pallet.TrayCount:N0}", labelFont, Brushes.Black, leftMargin, y);
        y += lineSpacing;

        e.Graphics.DrawString($"Scanned WO: {pallet.PalletScannedWO}", labelFont, Brushes.Black, leftMargin, y);
        y += lineSpacing;

        string packTime = pallet.PackedAt?.ToString("MM/dd/yyyy hh:mm tt") ?? "--";
        e.Graphics.DrawString($"PACK TIME: {packTime}", labelFont, Brushes.Black, leftMargin, y);
    }
}
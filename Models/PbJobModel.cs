
using System;
using System.Collections.Generic;
using System.Linq;
/*
public class PbJobModel
{
    public List<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();

    public string JobName { get; set; } = string.Empty;     // CAPONE
    public int JobNumber { get; set; }                      // 25367
    public DateTime PackDate { get; set; } = DateTime.Today;

    public int ScannedWorkOrders { get; set; } = 0;

    public List<Pallet> Pallets { get; set; } = new List<Pallet>();

    /// <summary>
    /// Default tray capacity used when a pallet's TrayCount is not set (>0).
    /// Adjust this as needed for your operation.
    /// </summary>
    public int TrayCapacity { get; set; } = 250;

    // Total envelopes = sum of all work order quantities across all pallets (null-safe)
    public int EnvelopeQty =>
        Pallets?
            .Where(p => p?.WorkOrders != null)
            .SelectMany(p => p.WorkOrders)
            .Sum(w => w?.Quantity ?? 0) ?? 0;

    /// <summary>
    /// Total trays across all pallets.
    /// Uses pallet.TrayCount when > 0, otherwise computes from pallet quantity and TrayCapacity.
    /// </summary>

    public int TotalTrays =>
        Pallets?
            .Where(p => p?.TrayCount > 0)
            .Sum(p => p.TrayCount)
        ?? 0;

    // Optional: total packed quantity = sum of per-pallet total quantities (null-safe)
    public int TotalPackedQty =>
        Pallets?.Sum(p => p?.TotalQuantity ?? 0) ?? 0;
}

public class WorkOrder
{
    public string Code { get; set; } = string.Empty; // CXXX26010101PER0001
    public int Quantity { get; set; }                // 150, 1500, etc.
    public bool IsSelected { get; set; }             // checkbox
    public int ScannedWorkOrders { get; set; } = 0;
}

public class Pallet
{
    public int PalletNumber { get; set; }              // 1, 2, 3
    public bool IsSelected { get; set; } = false;      // checkbox
    public int TrayCount { get; set; } = 0;            // manual entry; if <= 0, auto-compute is used

    public DateTime PackedTime { get; set; }
        = DateTime.Now.Date.AddHours(21).AddMinutes(30);


    public List<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();

    /// <summary>
    /// Sum of quantities on this pallet (null-safe).
    /// </summary>
    public int TotalQuantity =>
        WorkOrders?.Sum(w => w?.Quantity ?? 0) ?? 0;

    /// <summary>
    /// Returns TrayCount if > 0 (manual override).
    /// Otherwise computes trays using ceiling(TotalQuantity / capacity).
    /// </summary>
    public int EffectiveTrayCount(int capacity)
    {
        if (capacity <= 0) return TrayCount > 0 ? TrayCount : 0; // guard against bad config
        if (TrayCount > 0) return TrayCount;

        var qty = TotalQuantity;
        if (qty <= 0) return 0;

        // Ceil division without floating point: (a + b - 1) / b
        return (qty + capacity - 1) / capacity;
    }
}
*/
 
 
//===========================================================================================================================

// ================
// Aggregate Root: JOB PB
// ================
public class PbJobModel
{
    //public PbJobModel(string jobName, int jobNumber)
    //{
    //    JobName = jobName;
    //    JobNumber = jobNumber;
    //    isReady = 0;
    //}

    public DateTime PackDate { get; set; } = DateTime.Today;
    public string JobName { get; set; } = string.Empty;
    public int JobNumber { get; set; }

    public int isReady { get; set; }

    public int TotalEnvelopeOfJob =>
        Pallets?
            .SelectMany(p => p?.WorkOrders ?? Enumerable.Empty<WorkOrder>())
            .Sum(w => w?.EnvelopeQty ?? 0) ?? 0;

    /// <summary>Total trays across ALL pallets (sums Pallet.PalletTrays).</summary>
    public int TotalTraysOfJob =>
        Pallets?.Sum(p => p?.Trays ?? 0) ?? 0;

    /// <summary>Total scanned WO across ALL pallets (sums Pallet.PalletScannedWO).</summary>
    public int TotalScannedWOOfJob =>
        Pallets?.Sum(p => p?.PalletScannedWO ?? 0) ?? 0;

    public List<Pallet> Pallets { get; set; } = new List<Pallet>();

}

// ================
// Work Order (scan unit)
// ================
public class WorkOrder
{
    /// <summary>WO code (display with envelope value when scanned).</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>Envelope quantity for this WO (int; retrieved from DB).</summary>
    public int EnvelopeQty { get; set; }

    /// <summary>UI checkbox for manual selection (not related to scanning).</summary>
    public bool IsSelected { get; set; }

    /// <summary>Auto-incremented per scan (device triggers +1 per successful scan).</summary>
    public int ScannedWorkOrders { get; private set; } = 0;

    /// <summary>Manual trays input (used to pack the pallet).</summary>
    //   public int Trays { get; set; } = 0;

    /// <summary>Pack time (set when packing; default 9:30 PM today).</summary>



    /// <summary>
    /// Call this when the device scans THIS WO.
    /// It increments the scanned counter by 1 and (optionally) updates the envelope qty
    /// if the scanner/device provides it at scan-time.
    /// </summary>
    /// <param name="scannedEnvelopeQty">Optional envelope quantity from scanner (if provided at scan time).</param>
    public void RecordScan(int? scannedEnvelopeQty = null)
    {
        // Increment scanned WO counter (1 scan = +1)
        ScannedWorkOrders++;

        // If your flow updates/display envelope before OK, you can accept it here:
        if (scannedEnvelopeQty.HasValue && scannedEnvelopeQty.Value >= 0)
        {
            EnvelopeQty = scannedEnvelopeQty.Value;
        }
        // PackTime typically set when packing; leave as-is here.
    }
}

// ================
// Pallet (aggregates WOs)
// ================
public class Pallet
{

    public DateTime PackedTime { get; set; }
            = DateTime.Now.Date.AddHours(21).AddMinutes(30); // 9:30 PM default

    /// <summary>UI selection (not used for totals).</summary>
    public bool IsSelected { get; set; }
    public int Trays { get; set; } = 0;
    /// <summary>Work orders loaded to this pallet.</summary>
    public List<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();

    /// <summary>Total trays on this pallet (sum of WO.Trays; trays are manual input).</summary>
    /// 
    //time


    /// <summary>Total scanned WOs on this pallet (sum of WO.ScannedWorkOrders).</summary>
    public int PalletScannedWO =>
        WorkOrders?.Sum(w => w?.ScannedWorkOrders ?? 0) ?? 0;

    /// <summary>Total envelope qty on this pallet (sum of WO.EnvelopeQty). Handy for grids.</summary>
    public int PalletEnvelopeQty =>
        WorkOrders?.Sum(w => w?.EnvelopeQty ?? 0) ?? 0;
}

//Create one tiny class to represent the scanning session.
public class PalletScanSession
{

    // ✅ Explicitly "not yet committed"
    public int PendingScannedWO { get; private set; }
    public int PendingEnvelopeQty { get; private set; }

    public void RegisterScan(int envelopeQty)
    {
        PendingScannedWO++;
        PendingEnvelopeQty += envelopeQty;
    }

    public void Reset()
    {
        PendingScannedWO = 0;
        PendingEnvelopeQty = 0;
    }

}
//
//This avoids duplicating commit logic in the dialog.
public static class PalletScanCommitter
{
    public static void Commit(Pallet pallet, PalletScanSession session)
    {
        if (pallet == null || session == null)
            return;

        var wo = new WorkOrder
        {
            Code = "SCANNED-BATCH",
            EnvelopeQty = session.PendingEnvelopeQty
        };

        for (int i = 0; i < session.PendingScannedWO; i++)
            wo.RecordScan();

        pallet.WorkOrders.Add(wo);
    }
}



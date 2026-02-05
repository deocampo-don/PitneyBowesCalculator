
using System;
using System.Collections.Generic;
using System.Linq;

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
  //  public int ScannedWorkOrders { get; set; } = 0;
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

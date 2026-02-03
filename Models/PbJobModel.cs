using System;
using System.Collections.Generic;
using System.Linq;

public class PbJobModel
{


    public List<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();

    public string JobName { get; set; } = string.Empty;     // CAPONE
    public int JobNumber { get; set; }                      // 25367
    public DateTime PackDate { get; set; } = DateTime.Today;

    public int EnvelopeQty { get; set; } = 0;
    public int ScannedWorkOrders { get; set; } = 0;

    public List<Pallet> Pallets { get; set; }
        = new List<Pallet>();

    public int TotalPallets => Pallets?.Count ?? 0;

    // Optional totals
    public int TotalPackedQty =>
        Pallets.Sum(p => p.TotalQuantity);
}

public class WorkOrder
{
    public string Code { get; set; }      // CXXX26010101PER0001
    public int Quantity { get; set; }     // 150, 1500, etc.
    public bool IsSelected { get; set; }  // checkbox
}

public class Pallet
{
    public int PalletNumber { get; set; }          // 1, 2, 3
    public bool IsSelected { get; set; } = false;  // checkbox

    public DateTime PackedTime { get; set; } = DateTime.Now;
    public int TrayCount { get; set; } = 0;

    public List<WorkOrder> WorkOrders { get; set; }
        = new List<WorkOrder>();

    // 🔥 Computed value (optional but useful)
    public int TotalQuantity =>
        WorkOrders.Sum(w => w.Quantity);
}


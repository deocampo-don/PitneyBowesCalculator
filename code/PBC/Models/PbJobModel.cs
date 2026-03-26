using System;
using System.Collections.Generic;
using System.Linq;

#region Job (Aggregate Root)

public class PbJobModel
{
    // ===== Identity =====
    public int JobId { get; set; }

    // ===== Core Data =====
    public string JobName { get; set; }
    public int JobNumber { get; set; }
    public bool IsTemp { get; set; } = false;
    public bool IsActive { get; set; }

    // ===== Dates =====
    public DateTime? PackDate { get; set; }   // nullable

    public DateTime? ShippedDate { get; set; }
    public DateTime? LastUpdated { get; set; }


    // ===== Aggregates =====
    public List<Pallet> Pallets { get; set; }

    public PbJobModel()
    {
        JobName = string.Empty;
        PackDate = DateTime.Today;
        Pallets = new List<Pallet>();
    }

    // ===== Computed =====
    public int TotalEnvelopeOfJob
    {
        get
        {
            return Pallets
                .SelectMany(p => p.WorkOrders)
                .Sum(w => w.Quantity);
        }
    }

    public int TotalTraysOfJob
    {
        get
        {
            return Pallets.Sum(p => p.TrayCount);
        }
    }

    public DateTime? LastPackedTime
    {
        get
        {
            if (Pallets.Count == 0)
                return null;

            return Pallets.Max(p => p.PackedAt);
        }
    }

    public DateTime EffectivePackDate
    {
        get
        {
            return LastPackedTime
                ?? PackDate
                ?? DateTime.Today;
        }
    }

    public Pallet GetActivePallet()
    {
        return Pallets
            .FirstOrDefault(p => p.PackedAt == null);
    }

    //////////////////
    public IEnumerable<DateTime> ShipmentTimes
    {
        get
        {
            return Pallets
                .Where(p => p.ShippedAt.HasValue)
                .Select(p => p.ShippedAt.Value)
                .Distinct()
                .OrderByDescending(t => t);
        }
    }
}

#endregion

#region Pallet

public class Pallet
{
    // ===== Identity =====
    public int PalletId { get; set; }
    public int PBJobId { get; set; }

    // ===== Core Data =====
    public int PalletNumber { get; set; }

    public DateTime? PackedAt { get; set; }
    public DateTime? ShippedAt { get; set; }

    public int TrayCount { get; set; }

    public PalletState State { get; set; }
    public string JobNameSnapshot { get; set; }

    // ===== Aggregates =====
    public List<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();


    // ===== Computed =====
    public int PalletScannedWO
    {
        get { return WorkOrders?.Count ?? 0; }
    }

    public int PalletEnvelopeQty
    {
        get { return WorkOrders?.Sum(w => w.Quantity) ?? 0; }
    }

}

#endregion

#region Work Order

public class WorkOrder
{
    // ===== Identity =====
    public int Id { get; set; }
    public int PalletId { get; set; }

    // ===== Core Data =====
    public string Barcode { get; set; }
    public string WorkOrderCode { get; set; }

    public int Quantity { get; set; }

    // ===== Scan State =====
    public int ScannedCount { get; private set; }

    public WorkOrder(string workOrderCode, int quantity)
    {
        WorkOrderCode = workOrderCode;
        Quantity = quantity;
        ScannedCount = 0;
    }
    public void RecordScan()
    {
        ScannedCount++;
    }

    // ===== Helpers =====
    public bool IsComplete => ScannedCount >= Quantity;

    public int Remaining => Math.Max(0, Quantity - ScannedCount);
}

#endregion

#region Scan Session

public class PalletScanSession
{
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

#endregion

#region Domain Service

public static class PalletScanCommitter
{
    public static WorkOrder Commit(Pallet pallet, PalletScanSession session)
    {
        if (pallet == null || session == null || session.PendingScannedWO == 0)
            return null;

        var wo = new WorkOrder("SCANNED-BATCH", session.PendingEnvelopeQty);
        wo.PalletId = pallet.PalletId;

        for (int i = 0; i < session.PendingScannedWO; i++)
            wo.RecordScan();

        pallet.WorkOrders.Add(wo);
        session.Reset();

        return wo;
    }
}
public class ShipmentGroup
{
    public DateTime ShippedAt { get; set; }

    public List<Pallet> Pallets { get; set; } = new List<Pallet>();

    public int TotalTrays => Pallets.Sum(p => p.TrayCount);

    public int TotalEnvelopes =>
        Pallets.SelectMany(p => p.WorkOrders).Sum(w => w.Quantity);

    public List<int> JobNumbers =>
        Pallets.Select(p => p.PBJobId).Distinct().ToList();
}
#endregion

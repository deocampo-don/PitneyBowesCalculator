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
    public bool IsReady { get; set; } = false;

    // ===== Dates =====
    public DateTime? PackDate { get; set; }   // nullable

    public DateTime? ShippedDate { get; set; }

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
                .Sum(w => w.EnvelopeQty);
        }
    }

    public int TotalTraysOfJob
    {
        get
        {
            return Pallets.Sum(p => p.TrayCount);
        }
    }

    public int TotalScannedWOOfJob
    {
        get
        {
            return Pallets.Sum(p => p.PalletScannedWO);
        }
    }

    public DateTime? LastPackedTime
    {
        get
        {
            if (Pallets.Count == 0)
                return null;

            return Pallets.Max(p => p.PackedTime);
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
}

#endregion

#region Pallet

public class Pallet
{
    // ===== Identity =====
    public int PalletId { get; set; }
    public int JobId { get; set; }

    // ===== Core Data =====
    public int PalletNumber { get; set; }
    public DateTime? PackedTime { get; set; }

    public int TrayCount { get; set; }

    // ===== Aggregates =====
    public List<WorkOrder> WorkOrders { get; set; }

    public Pallet()
    {
        PackedTime = DateTime.Today.AddHours(21).AddMinutes(30);
        TrayCount = 0;
        WorkOrders = new List<WorkOrder>();
    }

    public int PalletScannedWO
    {
        get
        {
            return WorkOrders.Sum(w => w.ScannedWorkOrders);
        }
    }

    public int PalletEnvelopeQty
    {
        get
        {
            return WorkOrders.Sum(w => w.EnvelopeQty);
        }
    }
}

#endregion

#region Work Order

public class WorkOrder
{
    // ===== Identity =====
    public int PalletWorkOrderId { get; set; }
    public int PalletId { get; set; }

    // ===== Core Data =====
    public string WoCode { get; private set; }
    public int EnvelopeQty { get; private set; }

    // ===== Scan State =====
    public int ScannedWorkOrders { get; private set; }

    public WorkOrder(string woCode, int envelopeQty)
    {
        WoCode = woCode;
        EnvelopeQty = envelopeQty;
        ScannedWorkOrders = 0;
    }

    public void RecordScan()
    {
        ScannedWorkOrders++;
    }
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

#endregion

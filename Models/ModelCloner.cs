using System.Linq;

public static class ModelCloner
{
    public static PbJobModel CloneJob(PbJobModel job)
    {
        return new PbJobModel
        {
            JobId = job.JobId,
            JobName = job.JobName,
            JobNumber = job.JobNumber,
            IsTemp = job.IsTemp,
            LastUpdated = job.LastUpdated,
            ShippedDate = job.ShippedDate,
            Pallets = job.Pallets
                .Select(p => new Pallet
                {
                    PalletId = p.PalletId,
                    PBJobId = p.PBJobId,
                    PalletNumber = p.PalletNumber,
                    PackedAt = p.PackedAt,
                    ShippedAt = p.ShippedAt,
                    TrayCount = p.TrayCount,
                    State = p.State,
                    WorkOrders = p.WorkOrders
                        .Select(w => new WorkOrder(w.WorkOrderCode, w.Quantity)
                        {
                            Id = w.Id,
                            PalletId = w.PalletId
                        })
                        .ToList()
                })
                .ToList()
        };
    }
}
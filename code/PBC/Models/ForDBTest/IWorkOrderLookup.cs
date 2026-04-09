using System.Threading.Tasks;

namespace PitneyBowesCalculator.Models.ForDBTest
{
    public interface IWorkOrderLookup
    {
        Task<int> GetEnvelopeQtyAsync(string woCode);
    }
}

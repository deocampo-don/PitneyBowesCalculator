using System.Threading.Tasks;

namespace PitneyBowesCalculator.Models.ForDBTest
{
    public class TestWorkOrderLookup : IWorkOrderLookup
    {
        public Task<int> GetEnvelopeQtyAsync(string rawInput)
        {
            var parts = rawInput.Split('|');

            if (parts.Length != 2)
                return Task.FromResult(0);

            if (!int.TryParse(parts[1], out int qty))
                return Task.FromResult(0);

            return Task.FromResult(qty);
        }
    }
}

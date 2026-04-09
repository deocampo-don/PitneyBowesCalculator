using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitneyBowesCalculator.Models.ForDBTest
{
    public class TestScanResolver : IScanResolver
    {
        public Task<ScanResult> ResolveAsync(string rawInput)
        {
            var parts = rawInput.Split('|');

            if (parts.Length != 2)
                return Task.FromResult<ScanResult>(null);

            if (!int.TryParse(parts[1], out int qty))
                return Task.FromResult<ScanResult>(null);

            return Task.FromResult(new ScanResult
            {
                WoCode = parts[0].Trim(),
                EnvelopeQty = qty
            });
        }
    }
}

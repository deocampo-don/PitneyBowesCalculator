using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitneyBowesCalculator.Models.ForDBTest
{
    public interface IScanResolver
    {
        Task<ScanResult> ResolveAsync(string rawInput);
    }
}

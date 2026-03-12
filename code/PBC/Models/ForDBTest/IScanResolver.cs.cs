using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models.ForDBTest
{
    public interface IScanResolver
    {
        Task<ScanResult> ResolveAsync(string rawInput);
    }
}

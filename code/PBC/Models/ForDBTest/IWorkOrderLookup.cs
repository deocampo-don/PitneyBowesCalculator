using System.Threading.Tasks;

namespace WindowsFormsApp1.Services
{
    public interface IWorkOrderLookup
    {
        Task<int> GetEnvelopeQtyAsync(string woCode);
    }
}

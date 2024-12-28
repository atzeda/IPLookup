// BatchProcessingService/Services/Interfaces/ICacheClient.cs
using System.Threading.Tasks;
using IPLookupService.Models;

namespace BatchProcessingService.Services.Interfaces
{
    public interface ICacheClient
    {
        Task<IPDetails> GetIPDetailsAsync(string ipAddress);
        Task AddIPDetailsAsync(string ipAddress, IPDetails details);
    }
}

using IPLookupService.Models;

namespace IPLookupService.Services.Interfaces
{
    public interface IIPLookupService
    {
        Task<IPDetails> GetIPDetailsAsync(string ipAddress);
    }
}

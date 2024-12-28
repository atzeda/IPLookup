using IPLookupService.Models;
using IPLookupService.Services.Interfaces;
using IPLookupService.Exceptions;
using System.Net.Http.Json;

namespace IPLookupService.Services
{
    public class IPLookupService : IIPLookupService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public IPLookupService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IPDetails> GetIPDetailsAsync(string ipAddress)
        {
            try
            {
                var apiKey = _configuration["IPStack:ApiKey"];
                var url = $"http://api.ipstack.com/{ipAddress}?access_key={apiKey}";
                var response = await _httpClient.GetFromJsonAsync<IPDetails>(url);

                if (response == null)
                    throw new IPServiceNotAvailableException();

                return response;
            }
            catch (Exception)
            {
                throw new IPServiceNotAvailableException();
            }
        }
    }
}

// BatchProcessingService/Services/CacheClient.cs
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using BatchProcessingService.Services.Interfaces;
using IPLookupService.Models;

public class CacheClient : ICacheClient
{
    private readonly HttpClient _httpClient;

    public CacheClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://ipcacheservice/api/cache/");
    }

    public async Task<IPDetails> GetIPDetailsAsync(string ipAddress)
    {
        var response = await _httpClient.GetAsync(ipAddress);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IPDetails>(content);
        }
        return null; // Handle errors as needed
    }

    public async Task AddIPDetailsAsync(string ipAddress, IPDetails details)
    {
        var json = JsonSerializer.Serialize(details);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await _httpClient.PostAsync(ipAddress, content);
    }
}

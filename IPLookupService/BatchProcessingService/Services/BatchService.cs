using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatchProcessingService.Models;
using BatchProcessingService.Services.Interfaces;

public class BatchService : IBatchService
{
    private readonly ICacheClient _cacheClient;

    // In-memory tracking of batch statuses
    private readonly ConcurrentDictionary<Guid, BatchStatus> _batchStatuses;

    public BatchService(ICacheClient cacheClient)
    {
        _cacheClient = cacheClient;
        _batchStatuses = new ConcurrentDictionary<Guid, BatchStatus>();
    }

    public async Task<Guid> CreateBatchAsync(IEnumerable<string> ipAddresses)
    {
        var batchId = Guid.NewGuid();
        var batchStatus = new BatchStatus
        {
            BatchId = batchId,
            Total = ipAddresses.Count(),
            Processed = 0,
            IsCompleted = false
        };

        _batchStatuses[batchId] = batchStatus;

        // Process the batch asynchronously
        _ = Task.Run(() => ProcessBatchAsync(batchId, ipAddresses));

        return batchId;
    }

    public Task<BatchStatus> GetBatchStatusAsync(Guid batchId)
    {
        return Task.FromResult(_batchStatuses.TryGetValue(batchId, out var status) ? status : null);
    }

    private async Task ProcessBatchAsync(Guid batchId, IEnumerable<string> ipAddresses)
    {
        foreach (var ip in ipAddresses)
        {
            try
            {
                var cachedDetails = await _cacheClient.GetIPDetailsAsync(ip);
                if (cachedDetails == null)
                {
                    // Log or handle cache miss
                    Console.WriteLine($"IP {ip} not found in cache.");
                }
                else
                {
                    Console.WriteLine($"IP {ip} found in cache: {cachedDetails.City}, {cachedDetails.Country}");
                }

                // Update the batch status
                _batchStatuses[batchId].Processed++;
            }
            catch (Exception ex)
            {
                // Log errors without breaking the batch
                Console.WriteLine($"Error processing IP {ip}: {ex.Message}");
            }
        }

        _batchStatuses[batchId].IsCompleted = true;
    }
}

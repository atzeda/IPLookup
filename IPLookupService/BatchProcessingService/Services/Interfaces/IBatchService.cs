using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BatchProcessingService.Models;

namespace BatchProcessingService.Services.Interfaces
{
    public interface IBatchService
    {
        Task<Guid> CreateBatchAsync(IEnumerable<string> ipAddresses);
        Task<BatchStatus> GetBatchStatusAsync(Guid batchId);
    }
}

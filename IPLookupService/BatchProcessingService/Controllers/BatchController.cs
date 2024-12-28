using Microsoft.AspNetCore.Mvc;
using BatchProcessingService.Services.Interfaces;
using BatchProcessingService.Models;

namespace BatchProcessingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatchController : ControllerBase
    {
        private readonly IBatchService _batchService;

        public BatchController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        [HttpPost]
        public IActionResult CreateBatch([FromBody] string[] ipAddresses)
        {
            var batchId = _batchService.CreateBatchAsync(ipAddresses);
            return Ok(new { BatchId = batchId });
        }

        [HttpGet("{batchId}")]
        public IActionResult GetBatchStatus(Guid batchId)
        {
            var status = _batchService.GetBatchStatusAsync(batchId);
            if (status == null)
                return NotFound("Batch not found.");
            return Ok(status);
        }
    }
}

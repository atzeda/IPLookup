using Microsoft.AspNetCore.Mvc;
using IPLookupService.Services.Interfaces;
using IPLookupService.Exceptions;

namespace IPLookupService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IPLookupController : ControllerBase
    {
        private readonly IIPLookupService _ipLookupService;

        public IPLookupController(IIPLookupService ipLookupService)
        {
            _ipLookupService = ipLookupService;
        }

        [HttpGet("{ipAddress}")]
        public async Task<IActionResult> GetIPDetails(string ipAddress)
        {
            try
            {
                var details = await _ipLookupService.GetIPDetailsAsync(ipAddress);
                return Ok(details);
            }
            catch (IPServiceNotAvailableException)
            {
                return StatusCode(503, "IP lookup service is unavailable.");
            }
        }
    }
}

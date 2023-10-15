using ATOM.Core.DTOs.Request;
using ATOM.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATOM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WreckDemandController : ControllerBase
    {
        private readonly IWreckDemandService _wreckDemandService;

        public WreckDemandController(IWreckDemandService wreckDemandService)
        {
            _wreckDemandService = wreckDemandService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWreckDemand([FromBody] AddWreckDemandDto wreckDemandDto)
        {
            await _wreckDemandService.AddWreckDemand(wreckDemandDto);
            return Ok("Data successfully added !");
        }

        [HttpGet]
        public async Task<IActionResult> AverageWreckLocation()
        {
            var values = await _wreckDemandService.AverageWreckLocation();

            return Ok(new
            {
                AverageLatitude = values.AverageLatitude,
                AverageLongitude = values.AverageLongitude
            });
        }
    }
}

using ATOM.Core.DTOs;
using ATOM.Core.DTOs.Request;
using ATOM.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ATOM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WreckDemandController : ControllerBase
    {
        private readonly IWreckDemandService _wreckDemandService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WreckDemandController(IWreckDemandService wreckDemandService, IHttpContextAccessor httpContextAccessor)
        {
            _wreckDemandService = wreckDemandService;
            _httpContextAccessor = httpContextAccessor;
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

        [Authorize]
        [HttpGet]
        [Route("WreckOperation")]
        public async Task<IActionResult> GetWreckOperations()
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var values = await _wreckDemandService.GetWreckOperation(userId);
            return Ok(
                new
                {
                    Id = values.Item1.Id,
                    Latitude = values.Item1.Latitude,
                    Longitude = values.Item1.Longitude,
                    Distance = values.distance,
                    People = values.Item1.People,
                    District = values.Item1.District.Name,
                    County = values.Item1.District.County.Name,
                });

        }

        [Authorize]
        [HttpGet]
        [Route("ChangeOperationStatus")]
        public IActionResult ChangeStatus(int id)
        {
            _wreckDemandService.ChangeStatus(id);
            return Ok();
        }


        [Authorize]
        [HttpGet]
        [Route("PeopleLocation")]
        public async Task<IActionResult> GetPeopleLocation(int wreckId)
        {
            return Ok(await _wreckDemandService.GetPeopleLocation(wreckId));
        }


        [Authorize]
        [HttpDelete]
        [Route("RemovePeopleLocation")]
        public IActionResult RemoveWreck(int wreckId)
        {
            _wreckDemandService.RemoveWreck(wreckId);
            return Ok();
        }
    }
}

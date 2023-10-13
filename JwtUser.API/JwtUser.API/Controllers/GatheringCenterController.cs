using JwtUser.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtUser.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatheringCenterController : ControllerBase
    {
        private readonly IGatheringCenterService _gatheringCenterService;

        public GatheringCenterController(IGatheringCenterService gatheringCenterService)
        {
            _gatheringCenterService = gatheringCenterService;
        }


        [HttpPost]
        public async Task<IActionResult> GetNearestGatheringCenter(float longitude, float latitude)
        {
            return Ok(await _gatheringCenterService.NearGatheringCenter(longitude,latitude));
        }
    }
}

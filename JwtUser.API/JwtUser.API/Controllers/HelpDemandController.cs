using JwtUser.Core.Entities;
using JwtUser.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtUser.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpDemandController : ControllerBase
    {
        private readonly IHelpDemandService _helpDemandService;

        public HelpDemandController(IHelpDemandService helpDemandService)
        {
            _helpDemandService = helpDemandService;
        }

        [HttpPost]
        public async Task<IActionResult> AddHelpDemand(HelpDemand helpDemand)
        {
            await _helpDemandService.AddAsync(helpDemand);
            return Ok("Data added !");
        }
    }
}

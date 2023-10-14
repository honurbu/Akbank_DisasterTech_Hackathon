using AutoMapper;
using JwtUser.Core.DTOs.Request;
using JwtUser.Core.Entities;
using JwtUser.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtUser.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HelpDemandController : ControllerBase
    {
        private readonly IHelpDemandService _helpDemandService;
        private readonly IMapper _mapper;

        public HelpDemandController(IHelpDemandService helpDemandService, IMapper mapper)
        {
            _helpDemandService = helpDemandService;
            _mapper = mapper;
        }

        
        [HttpPost]
        public async Task<IActionResult> AddHelpDemand([FromBody] AddHelpDemandDto helpDemand)
        {
            await _helpDemandService.AddHelpDemand(helpDemand);
            return Ok("Data added!");
        }


        [HttpGet]
        public async Task<IActionResult> AverageHelpLocation(int categoryId)
        {
            var values = await _helpDemandService.AverageHelpLocation(categoryId);

            return Ok(new
            {
                AverageLatitude = values.AverageLatitude,
                AverageLongitude = values.AverageLongitude
            });
        }
    }
}

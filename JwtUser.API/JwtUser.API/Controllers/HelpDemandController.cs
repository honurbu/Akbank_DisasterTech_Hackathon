using AutoMapper;
using JwtUser.Core.DTOs.Request;
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
    }
}

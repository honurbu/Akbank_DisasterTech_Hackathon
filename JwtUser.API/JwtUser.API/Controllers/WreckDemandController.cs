﻿using JwtUser.Core.DTOs.Request;
using JwtUser.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtUser.API.Controllers
{
    [Route("api/[controller]")]
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
    }
}

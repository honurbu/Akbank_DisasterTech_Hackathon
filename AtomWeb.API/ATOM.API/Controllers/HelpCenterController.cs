using ATOM.Core.DTOs.Request;
using ATOM.Core.Entities;
using ATOM.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATOM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpCenterController : ControllerBase
    {
        private readonly IHelpCenterService _helpCenterService;
        private readonly IMapper _mapper;
        public HelpCenterController(IHelpCenterService helpCenterService, IMapper mapper)
        {
            _helpCenterService = helpCenterService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHelpCenter()
        {
            return Ok(await _helpCenterService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddHelpCenter(AddHelpCenterDto helpCenterDto)
        {
            var helpCenter = _mapper.Map<HelpCenter>(helpCenterDto);
            await _helpCenterService.AddAsync(helpCenter);
            return Ok("Added");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHelpCenter(int id)
        {
            var helpCenter = await _helpCenterService.GetByIdAsync(id);
            _helpCenterService.Remove(helpCenter);

            return Ok("Delete");
        }

        [HttpPut]
        public ActionResult UpdateHelpCenter(UpdateHelpCenterDto helpCenterDto)
        {
            var helpCenter = _mapper.Map<HelpCenter>(helpCenterDto);
            _helpCenterService.Update(helpCenter);
            return Ok("Update");
        }
    }
}

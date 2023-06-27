using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using BLL.Services.Implementation;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/platforms")]
    [Authorize]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformService _platformService;

        public PlatformsController(IPlatformService service)
        {
            _platformService = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<PlatformForResponceDto>>> GetPlatforms([FromQuery] PlatformParameters platformParameters)
        {
            var platforms = await _platformService.GetAllPlatformsAsync(platformParameters);
            platforms.WritePaginationData(Response.Headers);
            return Ok(platforms);
        }

        [HttpGet("{platformid}")]
        public async Task<ActionResult<PlatformForResponceDto>> GetPlatform(int platformid)
        {
            return Ok(await _platformService.GetPlatformByIdAsync(platformid));
        }

        [Authorize(Roles = "1")]
        [HttpPut]
        public async Task<IActionResult> PutPlatform(PlatformForUpdateDto platformForUpdate)
        {
            await _platformService.UpdatePlatformAsync(platformForUpdate);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<ActionResult<List<PlatformForResponceDto>>> PostPlatform(PlatformForCreationDto platform)
        {
            await _platformService.AddPlatformAsync(platform);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{platformid}")]
        public async Task<IActionResult> DeletePlatform(int platformid)
        {
            await _platformService.DeletePlatformAsync(platformid);
            return NoContent();
        }
    }
}
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/developers")]
    [Authorize]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DevelopersController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeveloperForResponceDto>>> GetDevelopers()
        {
            return Ok(await _developerService.GetAllDevelopersAsync());
        }

        [HttpGet("{developerid}")]
        public async Task<ActionResult<DeveloperForResponceDto>> GetDeveloper(int developerid)
        {
            return Ok(await _developerService.GetDeveloperByIdAsync(developerid));
        }

        [Authorize(Roles = "1")]
        [HttpPut]
        public async Task<IActionResult> PutDeveloper(DeveloperForUpdateDto developerForUpdate)
        {
            await _developerService.UpdateDeveloperAsync(developerForUpdate);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IActionResult> PostDeveloper(DeveloperForCreationDto developerForCreation)
        {
            await _developerService.AddDeveloperAsync(developerForCreation);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{developerid}")]
        public async Task<IActionResult> DeleteDeveloper(int developerid)
        {
            await _developerService.DeleteDeveloperAsync(developerid);
            return NoContent();
        }
    }
}
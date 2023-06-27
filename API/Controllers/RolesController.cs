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
    [Route("api/roles")]
    [Authorize(Roles = "1")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<RoleForResponceDto>>> GetRoles([FromQuery] RoleParameters roleParameters)
        {
            var roles = await _roleService.GetAllRolesAsync(roleParameters);
            roles.WritePaginationData(Response.Headers);
            return Ok(roles);
        }

        [HttpGet("{roleid}")]
        public async Task<ActionResult<RoleForResponceDto>> GetRole(int roleid)
        {
            return Ok(await _roleService.GetRoleByIdAsync(roleid));
        }

        [HttpPost]
        public async Task<IActionResult> PostRole(RoleForCreationDto roleForCreation)
        {
            await _roleService.AddRoleAsync(roleForCreation);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutRole(RoleForUpdateDto roleForUpdate)
        {
            await _roleService.UpdateRoleAsync(roleForUpdate);
            return NoContent();
        }

        [HttpDelete("{roleid}")]
        public async Task<IActionResult> DeleteRole(int roleid)
        {
            await _roleService.DeleteRoleAsync(roleid);
            return NoContent();
        }

    }
}
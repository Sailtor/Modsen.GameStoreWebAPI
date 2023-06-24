using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public async Task<ActionResult<IEnumerable<RoleForResponceDto>>> GetRoles()
        {
            return Ok(await _roleService.GetAllRolesAsync());
        }

        [HttpGet("{roleid}")]
        public async Task<ActionResult<RoleForResponceDto>> GetRole(int roleid)
        {
            return Ok(await _roleService.GetRoleByIdAsync(roleid));
        }

        [HttpPost]
        public async Task<IActionResult> PostRole(RoleForCreationDto roleForCreation)
        {
            var context = new ValidationContext(roleForCreation);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(roleForCreation, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                Console.WriteLine();
            }
            else
                await _roleService.AddRoleAsync(roleForCreation);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutRole(RoleForUpdateDto roleForUpdate)
        {
            var context = new ValidationContext(roleForUpdate);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(roleForUpdate, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                Console.WriteLine();
            }
            else
                await _roleService.UpdateRoleAsync(roleForUpdate);
            return NoContent();
        }

        [HttpDelete("{roleid}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleService.DeleteRoleAsync(id);
            return NoContent();
        }

    }
}
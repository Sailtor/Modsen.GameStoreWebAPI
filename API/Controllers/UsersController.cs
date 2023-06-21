using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserForResponceDto>>> GetUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("{userid}")]
        public async Task<ActionResult<UserForResponceDto>> GetUser(int userid)
        {
            return Ok(await _userService.GetUserByIdAsync(userid));
        }

        [HttpPut("{userid}")]
        public async Task<IActionResult> PutUser(int userid, UserForCreationDto userForCreation)
        {
            if (HttpContext.User.FindFirstValue(ClaimTypes.Role) != "1")
            {
                int tokenUserId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));

                if (tokenUserId != userid)
                {
                    return Unauthorized();
                }
            }
            await _userService.UpdateUserAsync(userForCreation, userid);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpPut("{userid}/roles/{roleid}")]
        public async Task<IActionResult> PutRoleInUser(int userid, int roleid)
        {
            await _userService.UpdateUserRoleAsync(userid, roleid);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserForResponceDto>> Register(UserForCreationDto userForCreation)
        {
            await _userService.RegisterUserAsync(userForCreation);
            return Ok();
        }

        [HttpDelete("{userid}")]
        public async Task<IActionResult> DeleteRole(int userid)
        {
            if (HttpContext.User.FindFirstValue(ClaimTypes.Role) != "1")
            {
                int tokenUserId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));

                if (tokenUserId != userid)
                {
                    return Unauthorized();
                }
            }
            await _userService.DeleteUserAsync(userid);
            return NoContent();
        }

        /* --- TOUCHED (de)GENERATED CODE UP THERE --- */
    }
}

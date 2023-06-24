using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UsersController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
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

        [HttpPut]
        public async Task<IActionResult> PutUser(UserForUpdateDto userForUpdate)
        {
            _authService.CheckAuthorization(userForUpdate.Id, User);
            await _userService.UpdateUserAsync(userForUpdate);
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
        public async Task<ActionResult> Register(UserForCreationDto userForCreation)
        {
            await _userService.RegisterUserAsync(userForCreation);
            return Ok();
        }

        [HttpDelete("{userid}")]
        public async Task<IActionResult> DeleteRole(int userid)
        {
            _authService.CheckAuthorization(userid, User);
            await _userService.DeleteUserAsync(userid);
            return NoContent();
        }

        /* --- TOUCHED (de)GENERATED CODE UP THERE --- */
    }
}

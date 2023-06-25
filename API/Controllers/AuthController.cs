using BLL.Dtos.InDto;
using BLL.Infrastructure.Logger;
using BLL.Services.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILoggerManager _logger;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticatedResponse>> Login(UserForLoginDto creds)
        {
            return Ok(await _authService.Login(creds));
        }
    }
}
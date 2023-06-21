using BLL.Services.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken(TokenApiModel tokenApiModel)
        {
            await _tokenService.RefreshTokenAsync(tokenApiModel);
            return Ok();
        }

        [HttpPost]
        [Route("revoketoken")]
        public async Task<IActionResult> RevokeToken()
        {
            await _tokenService.RevokeTokenAsync(User);
            return Ok();
        }
    }
}
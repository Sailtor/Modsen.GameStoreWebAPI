using BLL.Services.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
        public async Task<ActionResult<AuthenticatedResponse>> RefreshToken(TokenApiModel tokenApiModel)
        {
            return Ok(await _tokenService.RefreshTokenAsync(tokenApiModel));
        }

        [Authorize]
        [HttpPost]
        [Route("revoketoken")]
        public async Task<IActionResult> RevokeToken()
        {
            await _tokenService.RevokeTokenAsync(User);
            return Ok();
        }
    }
}
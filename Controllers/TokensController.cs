using GameStoreWebAPI.Models;
using GameStoreWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly GameStoreDBContext _context;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public TokenController(GameStoreDBContext userContext, ITokenService tokenService, IConfiguration configuration)
        {
            _context = userContext;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("api/users/refreshtoken")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken, _configuration.GetValue<string>("Jwt Settings:Key:GameStoreWebAPIKey"));

            var userID = principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value; //this is mapped to the Name claim by default
            var user = _context.Users.SingleOrDefault(u => u.Id == Convert.ToInt32(userID));

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims, _configuration.GetValue<string>("Jwt Settings:Key:GameStoreWebAPIKey"));
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            _userContext.SaveChanges();

            return Ok(new AuthenticatedResponse()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
        [HttpPost, Authorize]
        [Route("api/users/revoke")]
        public IActionResult Revoke()
        {
            var userID = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value; //this is mapped to the Name claim by default
            var user = _context.Users.SingleOrDefault(u => u.Id == Convert.ToInt32(userID));

            if (user == null) return BadRequest();

            user.RefreshToken = null;
            _userContext.SaveChanges();

            return NoContent();
        }
    }
}
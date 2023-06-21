using BLL.Services.Contracts;
using DAL.Data;
using DAL.Models;
using GameStoreWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize(Roles = "1,2")]
    [Route("api/users")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly GameStoreDBContext _context;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public TokenController(GameStoreDBContext context, ITokenService tokenService, IConfiguration configuration)
        {
            _context = context;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("refreshtoken")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken, _configuration.GetValue<string>("Jwt Settings:Key"));

            var userID = principal.FindFirstValue("UserID");
            var user = _context.Users.SingleOrDefault(u => u.Id == Convert.ToInt32(userID));

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims, _configuration.GetValue<string>("Jwt Settings:Key"));
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            _context.SaveChanges();

            return Ok(new AuthenticatedResponse()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var userID = User.FindFirstValue("UserID");
            var user = _context.Users.SingleOrDefault(u => u.Id == Convert.ToInt32(userID));

            if (user == null) return BadRequest();

            user.RefreshToken = null;
            _context.SaveChanges();

            return NoContent();
        }
    }
}
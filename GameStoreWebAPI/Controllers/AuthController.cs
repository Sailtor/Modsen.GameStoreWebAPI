using GameStoreWebAPI.Models;
using GameStoreWebAPI.Models.Dtos.In;
using GameStoreWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameStoreWebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GameStoreDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AuthController(GameStoreDBContext context, IConfiguration configuration, ITokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserForLoginDto creds)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Login == creds.Login);
            if (user == null)
            {
                return BadRequest("User with this login not found");
            }
            if (!(user.Password == creds.Password))
            {
                return BadRequest("Wrong password");
            }

            List<Claim> claims = new()
            {
                new Claim("UserID", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var accessToken = _tokenService.GenerateAccessToken(claims, _configuration.GetValue<string>("Jwt Settings:Key"));
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            _context.SaveChanges();

            return Ok(new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}
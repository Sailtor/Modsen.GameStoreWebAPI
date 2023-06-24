using BLL.Dtos.InDto;
using BLL.Exceptions;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repository.UnitOfWork;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace BLL.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<AuthenticatedResponse> Login(UserForLoginDto creds)
        {
            var user = (await _unitOfWork.User.FindAsync(u => u.Login == creds.Login)).First();
            if (!(user.Password == creds.Password))
            {
                throw new WrongPasswordException();
            }

            List<Claim> claims = new()
            {
                new Claim("UserID", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var accessToken = _tokenService.GenerateAccessToken(claims, _configuration.GetRequiredSection("Jwt settings:Key").Value);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _unitOfWork.SaveAsync();

            return new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken
            };
        }

        public void CheckAuthorization(int userid, ClaimsPrincipal user)
        {
            if (user.FindFirst(ClaimTypes.Role).Value != "1")
            {
                int tokenUserId = Convert.ToInt32(user.FindFirst("UserID").Value);

                if (tokenUserId != userid)
                {
                    throw new UserUnauthorizedException();
                }
            }
        }
    }
}

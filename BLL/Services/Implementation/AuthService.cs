using BLL.Dtos.InDto;
using BLL.Exceptions;
using BLL.Infrastructure.Validators;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using DAL.Repository.UnitOfWork;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace BLL.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IValidator<UserForLoginDto> _userLoginValidator;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, ITokenService tokenService, IValidator<UserForLoginDto> userLoginValidator)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _tokenService = tokenService;
            _userLoginValidator = userLoginValidator;
        }

        public async Task<AuthenticatedResponse> Login(UserForLoginDto creds)
        {
            _userLoginValidator.ValidateAndThrowCustom(creds);
            var user = (await _unitOfWork.User.FindAsync(u => u.Login == creds.Login, new UserParameters() { PageNumber = 1, PageSize = 1 })).First();
            if (!BCrypt.Net.BCrypt.EnhancedVerify(creds.Password, user.Password))
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
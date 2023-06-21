using BLL.Exceptions;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public TokenService(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims, string APIkey)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(APIkey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string APIkey)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(APIkey)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public async Task<AuthenticatedResponse> RefreshTokenAsync(TokenApiModel tokenApiModel)
        {
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken, _configuration.GetRequiredSection("Jwt settings:Key").Value);

            var userID = principal.FindFirst("UserID").Value;
            var user = await _userService.GetFullUserByIdAsync(Convert.ToInt32(userID));

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new InvalidRequestException();
            }

            var newAccessToken = GenerateAccessToken(principal.Claims, _configuration.GetRequiredSection("Jwt settings:Key").Value);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _userService.SaveAsync();

            return new AuthenticatedResponse()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task RevokeTokenAsync(ClaimsPrincipal User)
        {
            var userID = User.FindFirst("UserID").Value;
            var user = await _userService.GetFullUserByIdAsync(Convert.ToInt32(userID));

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await _userService.SaveAsync();
        }
    }
}
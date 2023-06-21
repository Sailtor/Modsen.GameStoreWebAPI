using DAL.Models;
using System.Security.Claims;

namespace BLL.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims, string APIkey);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string APIkey);
        Task<AuthenticatedResponse> RefreshTokenAsync(TokenApiModel tokenApiModel);
        Task RevokeTokenAsync(ClaimsPrincipal User);
    }
}
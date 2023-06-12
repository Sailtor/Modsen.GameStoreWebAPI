using System.Security.Claims;

namespace GameStoreWebAPI.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims, string APIkey);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string APIkey);
    }
}
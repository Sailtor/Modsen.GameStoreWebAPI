using BLL.Dtos.InDto;
using DAL.Models;
using System.Security.Claims;

namespace BLL.Services.Contracts
{
    public interface IAuthService
    {
        Task<AuthenticatedResponse> Login(UserForLoginDto creds);
        void CheckAuthorization(int userid, ClaimsPrincipal user);
    }
}
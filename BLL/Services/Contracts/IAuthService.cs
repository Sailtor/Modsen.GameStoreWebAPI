using BLL.Dtos.InDto;
using DAL.Models;

namespace BLL.Services.Contracts
{
    internal interface IAuthService
    {
        Task<AuthenticatedResponse> Login(UserForLoginDto creds);
    }
}

using BLL.Dtos.InDto;
using DAL.Models;

namespace BLL.Services.Contracts
{
    public interface IAuthService
    {
        Task<AuthenticatedResponse> Login(UserForLoginDto creds);
    }
}

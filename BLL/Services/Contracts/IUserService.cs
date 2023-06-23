using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;

namespace BLL.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserForResponceDto>> GetAllUsersAsync();
        Task<UserForResponceDto> GetUserByIdAsync(int userid);
        Task<User> GetFullUserByIdAsync(int userid);
        Task RegisterUserAsync(UserForCreationDto user);
        Task UpdateUserRoleAsync(int userid, int roleid);
        Task UpdateUserAsync(UserForCreationDto user, int userid);
        Task DeleteUserAsync(int userid);
        Task SaveAsync();
    }
}
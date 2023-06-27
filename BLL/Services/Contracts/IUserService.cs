using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace BLL.Services.Contracts
{
    public interface IUserService
    {
        Task<PagedList<UserForResponceDto>> GetAllUsersAsync(UserParameters parameters);
        Task<UserForResponceDto> GetUserByIdAsync(int userid);
        Task<User> GetFullUserByIdAsync(int userid);
        Task RegisterUserAsync(UserForCreationDto user);
        Task UpdateUserRoleAsync(int userid, int roleid);
        Task UpdateUserAsync(UserForUpdateDto user);
        Task DeleteUserAsync(int userid);
        Task SaveAsync();
    }
}
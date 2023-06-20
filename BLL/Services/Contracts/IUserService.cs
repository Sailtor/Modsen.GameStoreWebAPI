using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IUserService
    {
        public Task<IEnumerable<UserForResponceDto>> GetAllUsersAsync();
        public Task<UserForResponceDto> GetUseryIdAsync(int userid);
        public Task RegisterUserAsync(UserForCreationDto user); //не уверен вообще
        public Task UpdateUserRoleAsync(int userid, int roleid);
        public Task UpdateUserAsync(int userid);
        public Task DeleteUserAsync(int userid);
    }
}

using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IRoleService
    {
        public Task<IEnumerable<RoleForResponceDto>> GetAllRolesAsync();
        public Task<RoleForResponceDto> GetRoleByIdAsync(int roleid);
        public Task AddRoleAsync(RoleForCreationDto role);
        public Task UpdateRoleAsync(int roleid, RoleForCreationDto role);
        public Task DeleteRoleAsync(int roleid);
    }
}
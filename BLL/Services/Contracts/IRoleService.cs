using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace BLL.Services.Contracts
{
    public interface IRoleService
    {
        Task<PagedList<RoleForResponceDto>> GetAllRolesAsync(RoleParameters parameters);
        Task<RoleForResponceDto> GetRoleByIdAsync(int roleid);
        Task AddRoleAsync(RoleForCreationDto role);
        Task UpdateRoleAsync(RoleForUpdateDto roleForUpdate);
        Task DeleteRoleAsync(int roleid);
    }
}
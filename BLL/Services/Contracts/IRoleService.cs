﻿using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleForResponceDto>> GetAllRolesAsync();
        Task<RoleForResponceDto> GetRoleByIdAsync(int roleid);
        Task AddRoleAsync(RoleForCreationDto role);
        Task UpdateRoleAsync(RoleForUpdateDto roleForUpdate);
        Task DeleteRoleAsync(int roleid);
    }
}
﻿using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IUserService
    {
        public Task<IEnumerable<UserForResponceDto>> GetAllUsersAsync();
        public Task<UserForResponceDto> GetUserByIdAsync(int userid);
        public Task RegisterUserAsync(UserForCreationDto user);
        public Task UpdateUserRoleAsync(int userid, int roleid);
        public Task UpdateUserAsync(int userid);
        public Task DeleteUserAsync(int userid);
    }
}
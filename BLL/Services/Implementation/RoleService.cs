using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repository.UnitOfWork;

namespace BLL.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoleForResponceDto>> GetAllRolesAsync()
        {
            return _mapper.Map<IEnumerable<RoleForResponceDto>>(await _unitOfWork.Role.GetAllAsync());
        }

        public async Task<RoleForResponceDto> GetRoleByIdAsync(int roleid)
        {
            return _mapper.Map<RoleForResponceDto>(await _unitOfWork.Role.GetByIdAsync(roleid));
        }

        public async Task AddRoleAsync(RoleForCreationDto roleForCreation)
        {
            await _unitOfWork.Role.AddAsync(_mapper.Map<Role>(roleForCreation));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateRoleAsync(int roleid, RoleForCreationDto roleForCreation)
        {
            var role = await _unitOfWork.Role.GetByIdAsync(roleid);
            _mapper.Map(roleForCreation, role);
            await _unitOfWork.SaveAsync();
        }
        public async Task DeleteRoleAsync(int roleid)
        {
            _ = await _unitOfWork.Role.GetByIdAsync(roleid);
            await _unitOfWork.Role.Delete(roleid);
            await _unitOfWork.SaveAsync();
        }
    }
}

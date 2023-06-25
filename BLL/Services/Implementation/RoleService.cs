using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Infrastructure.Validators;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repository.UnitOfWork;
using FluentValidation;

namespace BLL.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<RoleForCreationDto> _creationValidator;
        private readonly IValidator<RoleForUpdateDto> _updateValidator;

        public RoleService(IMapper mapper, IUnitOfWork unitOfWork, IValidator<RoleForCreationDto> creationValidator, IValidator<RoleForUpdateDto> updateValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _creationValidator = creationValidator;
            _updateValidator = updateValidator;
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
            _creationValidator.ValidateAndThrowCustom(roleForCreation);
            await _unitOfWork.Role.AddAsync(_mapper.Map<Role>(roleForCreation));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateRoleAsync(RoleForUpdateDto roleForUpdate)
        {
            _updateValidator.ValidateAndThrowCustom(roleForUpdate);
            Role role = await _unitOfWork.Role.GetByIdAsync(roleForUpdate.Id);
            _mapper.Map(roleForUpdate, role);
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

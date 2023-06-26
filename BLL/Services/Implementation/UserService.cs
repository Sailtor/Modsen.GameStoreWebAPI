using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Infrastructure.Validators;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repository.UnitOfWork;
using FluentValidation;
using BCrypt.Net;

namespace BLL.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<UserForCreationDto> _creationValidator;
        private readonly IValidator<UserForUpdateDto> _updateValidator;
        public UserService(IMapper mapper, IUnitOfWork unitOfWork, IValidator<UserForCreationDto> creationValidator, IValidator<UserForUpdateDto> updateValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _creationValidator = creationValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IEnumerable<UserForResponceDto>> GetAllUsersAsync()
        {
            return _mapper.Map<IEnumerable<UserForResponceDto>>(await _unitOfWork.User.GetAllAsync());
        }

        public async Task<UserForResponceDto> GetUserByIdAsync(int userid)
        {
            return _mapper.Map<UserForResponceDto>(await _unitOfWork.User.GetByIdAsync(userid));
        }

        public async Task<User> GetFullUserByIdAsync(int userid)
        {
            return await _unitOfWork.User.GetByIdAsync(userid);
        }

        public async Task RegisterUserAsync(UserForCreationDto userForCreation)
        {
            _creationValidator.ValidateAndThrowCustom(userForCreation);
            userForCreation.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(userForCreation.Password);
            await _unitOfWork.User.AddAsync(_mapper.Map<User>(userForCreation));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateUserAsync(UserForUpdateDto userForUpdate)
        {
            _updateValidator.ValidateAndThrowCustom(userForUpdate);
            userForUpdate.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(userForUpdate.Password);
            User user = await _unitOfWork.User.GetByIdAsync(userForUpdate.Id);
            _mapper.Map(userForUpdate, user);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateUserRoleAsync(int userid, int roleid)
        {
            User userEntity = await _unitOfWork.User.GetByIdAsync(userid);
            Role roleEntity = await _unitOfWork.Role.GetByIdAsync(roleid);
            userEntity.RoleId = roleEntity.Id;
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUserAsync(int userid)
        {
            _ = await _unitOfWork.User.GetByIdAsync(userid);
            await _unitOfWork.User.Delete(userid);
            await _unitOfWork.SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveAsync();
        }
    }
}

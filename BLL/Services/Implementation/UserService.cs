using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repository.UnitOfWork;

namespace BLL.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

        public async Task RegisterUserAsync(UserForCreationDto user)
        {
            await _unitOfWork.User.AddAsync(_mapper.Map<User>(user));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateUserAsync(UserForUpdateDto userForUpdate)
        {
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

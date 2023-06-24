using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repository.UnitOfWork;

namespace BLL.Services.Implementation
{
    public class PlatformService : IPlatformService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlatformService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PlatformForResponceDto>> GetAllPlatformsAsync()
        {
            return _mapper.Map<IEnumerable<PlatformForResponceDto>>(await _unitOfWork.Platform.GetAllAsync());
        }

        public async Task<PlatformForResponceDto> GetPlatformByIdAsync(int platformid)
        {
            return _mapper.Map<PlatformForResponceDto>(await _unitOfWork.Platform.GetByIdAsync(platformid));
        }

        public async Task AddPlatformAsync(PlatformForCreationDto platformForCreation)
        {
            await _unitOfWork.Platform.AddAsync(_mapper.Map<Platform>(platformForCreation));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdatePlatformAsync(PlatformForUpdateDto platformForUpdate)
        {
            Platform platform = await _unitOfWork.Platform.GetByIdAsync(platformForUpdate.Id);
            _mapper.Map(platformForUpdate, platform);
            await _unitOfWork.SaveAsync();
        }
        public async Task DeletePlatformAsync(int platformid)
        {
            _ = await _unitOfWork.Platform.GetByIdAsync(platformid);
            await _unitOfWork.Platform.Delete(platformid);
            await _unitOfWork.SaveAsync();
        }
    }
}

using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Infrastructure.Validators;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using DAL.Repository.UnitOfWork;
using FluentValidation;

namespace BLL.Services.Implementation
{
    public class PlatformService : IPlatformService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<PlatformForCreationDto> _creationValidator;
        private readonly IValidator<PlatformForUpdateDto> _updateValidator;
        public PlatformService(IMapper mapper, IUnitOfWork unitOfWork, IValidator<PlatformForCreationDto> creationValidator, IValidator<PlatformForUpdateDto> updateValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _creationValidator = creationValidator;
            _updateValidator = updateValidator;
        }

        public async Task<PagedList<PlatformForResponceDto>> GetAllPlatformsAsync(PlatformParameters parameters)
        {
            return _mapper.Map<PagedList<PlatformForResponceDto>>(_unitOfWork.Platform.GetAllPlatforms(parameters));
        }

        public async Task<PlatformForResponceDto> GetPlatformByIdAsync(int platformid)
        {
            return _mapper.Map<PlatformForResponceDto>(await _unitOfWork.Platform.GetByIdAsync(platformid));
        }

        public async Task AddPlatformAsync(PlatformForCreationDto platformForCreation)
        {
            _creationValidator.ValidateAndThrowCustom(platformForCreation);
            await _unitOfWork.Platform.AddAsync(_mapper.Map<Platform>(platformForCreation));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdatePlatformAsync(PlatformForUpdateDto platformForUpdate)
        {
            _updateValidator.ValidateAndThrowCustom(platformForUpdate);
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

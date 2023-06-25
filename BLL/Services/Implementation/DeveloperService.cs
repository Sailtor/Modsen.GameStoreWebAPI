using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repository.UnitOfWork;
using FluentValidation;

namespace BLL.Services.Implementation
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<DeveloperForCreationDto> _creationValidator;
        private readonly IValidator<DeveloperForUpdateDto> _updateValidator;

        public DeveloperService(IMapper mapper, IUnitOfWork unitOfWork, IValidator<DeveloperForCreationDto> creationValidator, IValidator<DeveloperForUpdateDto> updateValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _creationValidator = creationValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IEnumerable<DeveloperForResponceDto>> GetAllDevelopersAsync()
        {
            return _mapper.Map<IEnumerable<DeveloperForResponceDto>>(await _unitOfWork.Developer.GetAllAsync());
        }

        public async Task<DeveloperForResponceDto> GetDeveloperByIdAsync(int developerid)
        {
            return _mapper.Map<DeveloperForResponceDto>(await _unitOfWork.Developer.GetByIdAsync(developerid));
        }

        public async Task AddDeveloperAsync(DeveloperForCreationDto developerForCreation)
        {
            _creationValidator.ValidateAndThrow(developerForCreation);
            await _unitOfWork.Developer.AddAsync(_mapper.Map<Developer>(developerForCreation));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateDeveloperAsync(DeveloperForUpdateDto developerForCreation)
        {
            Developer developer = await _unitOfWork.Developer.GetByIdAsync(developerForCreation.Id);
            _mapper.Map(developerForCreation, developer);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteDeveloperAsync(int developerid)
        {
            _ = await _unitOfWork.Developer.GetByIdAsync(developerid);
            await _unitOfWork.Developer.Delete(developerid);
            await _unitOfWork.SaveAsync();
        }
    }
}

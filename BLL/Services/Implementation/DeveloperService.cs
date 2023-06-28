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

        public async Task<PagedList<DeveloperForResponceDto>> GetAllDevelopersAsync(DeveloperParameters parameters)
        {
            return _mapper.Map<PagedList<DeveloperForResponceDto>>(_unitOfWork.Developer.GetAllDevelopers(parameters));
        }

        public async Task<DeveloperForResponceDto> GetDeveloperByIdAsync(int developerid)
        {
            return _mapper.Map<DeveloperForResponceDto>(await _unitOfWork.Developer.GetByIdAsync(developerid));
        }

        public async Task AddDeveloperAsync(DeveloperForCreationDto developerForCreation)
        {
            _creationValidator.ValidateAndThrowCustom(developerForCreation);
            await _unitOfWork.Developer.AddAsync(_mapper.Map<Developer>(developerForCreation));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateDeveloperAsync(DeveloperForUpdateDto developerForUpdate)
        {
            _updateValidator.ValidateAndThrowCustom(developerForUpdate);
            Developer developer = await _unitOfWork.Developer.GetByIdAsync(developerForUpdate.Id);
            _mapper.Map(developerForUpdate, developer);
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

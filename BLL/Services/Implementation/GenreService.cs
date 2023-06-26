﻿using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Infrastructure.Validators;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repository.UnitOfWork;
using FluentValidation;

namespace BLL.Services.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<GenreForCreationDto> _creationValidator;
        private readonly IValidator<GenreForUpdateDto> _updateValidator;
        public GenreService(IMapper mapper, IUnitOfWork unitOfWork, IValidator<GenreForCreationDto> creationValidator, IValidator<GenreForUpdateDto> updateValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _creationValidator = creationValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IEnumerable<GenreForResponceDto>> GetAllGenresAsync()
        {
            return _mapper.Map<IEnumerable<GenreForResponceDto>>(await _unitOfWork.Genre.GetAllAsync());
        }

        public async Task<GenreForResponceDto> GetGenreByIdAsync(int genreid)
        {
            return _mapper.Map<GenreForResponceDto>(await _unitOfWork.Genre.GetByIdAsync(genreid));
        }

        public async Task AddGenreAsync(GenreForCreationDto genreForCreation)
        {
            _creationValidator.ValidateAndThrowCustom(genreForCreation);
            await _unitOfWork.Genre.AddAsync(_mapper.Map<Genre>(genreForCreation));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateGenreAsync(GenreForUpdateDto genreForUpdate)
        {
            _updateValidator.ValidateAndThrowCustom(genreForUpdate);
            Genre genre = await _unitOfWork.Genre.GetByIdAsync(genreForUpdate.Id);
            _mapper.Map(genreForUpdate, genre);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteGenreAsync(int genreid)
        {
            _ = await _unitOfWork.Genre.GetByIdAsync(genreid);
            await _unitOfWork.Genre.Delete(genreid);
            await _unitOfWork.SaveAsync();
        }
    }
}

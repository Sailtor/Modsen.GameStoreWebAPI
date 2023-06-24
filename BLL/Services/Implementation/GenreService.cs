using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repository.UnitOfWork;

namespace BLL.Services.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
            await _unitOfWork.Genre.AddAsync(_mapper.Map<Genre>(genreForCreation));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateGenreAsync(GenreForUpdateDto genreForUpdate)
        {
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

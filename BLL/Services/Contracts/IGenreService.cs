using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IGenreService
    {
        public Task<IEnumerable<GenreForResponceDto>> GetAllGenresAsync();
        public Task<GenreForResponceDto> GetGenreByIdAsync(int genreid);
        public Task AddGenreAsync(GenreForCreationDto genre);
        public Task UpdateGenreAsync(int genreid, GenreForCreationDto genre);
        public Task DeleteGenreAsync(int genreid);
    }
}
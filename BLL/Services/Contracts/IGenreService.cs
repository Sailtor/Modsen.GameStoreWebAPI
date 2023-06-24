using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreForResponceDto>> GetAllGenresAsync();
        Task<GenreForResponceDto> GetGenreByIdAsync(int genreid);
        Task AddGenreAsync(GenreForCreationDto genre);
        Task UpdateGenreAsync(GenreForUpdateDto genreForUpdate);
        Task DeleteGenreAsync(int genreid);
    }
}
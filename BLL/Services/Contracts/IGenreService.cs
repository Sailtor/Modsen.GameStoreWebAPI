using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace BLL.Services.Contracts
{
    public interface IGenreService
    {
        Task<PagedList<GenreForResponceDto>> GetAllGenresAsync(GenreParameters parameters);
        Task<GenreForResponceDto> GetGenreByIdAsync(int genreid);
        Task AddGenreAsync(GenreForCreationDto genre);
        Task UpdateGenreAsync(GenreForUpdateDto genreForUpdate);
        Task DeleteGenreAsync(int genreid);
    }
}
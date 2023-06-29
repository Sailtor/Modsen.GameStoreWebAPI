using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace DAL.Repository.Contracts
{
    public interface IGenreRepository : IRepository<Genre, int>
    {
        Task<PagedList<Genre>> GetAllGenresAsync(GenreParameters parameters);
    }
}
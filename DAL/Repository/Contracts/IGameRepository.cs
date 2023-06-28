using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace DAL.Repository.Contracts
{
    public interface IGameRepository : IRepository<Game, int>
    {
        Task<PagedList<Game>> GetAllFilteredAsync(GameParameters parameters);
        Task<PagedList<Game>> GetAllIncludeAllAsync(GameParameters parameters);
        Task<Game> GetByIdIncludeAllAsync(int gameid);
    }
}

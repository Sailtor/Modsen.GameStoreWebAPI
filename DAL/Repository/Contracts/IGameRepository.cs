using DAL.Models;

namespace DAL.Repository.Contracts
{
    public interface IGameRepository : IRepository<Game,int>
    {
        Task<Game> GetByIdIncludeAllAsync(int gameid);
    }
}

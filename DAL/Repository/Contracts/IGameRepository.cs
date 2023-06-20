using DAL.Models;

namespace DAL.Repository.Contracts
{
    public interface IGameRepository : IRepository<Game,int>
    {
        public Task<Game> GetByIdIncludeAsync(int gameid);
        /*
        public void AddGameGenreAsync(int gameid, int genreid);
        public void AddGamePlatformAsync(int gameid, int platformid);
        public void RemoveGameGenreAsync(int gameid, int genreid);
        public void RemoveGamePlatformAsync(int gameid, int platformid);
        */
    }
}

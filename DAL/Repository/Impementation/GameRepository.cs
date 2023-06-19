using DAL.Repository.Contracts;
using GameStoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class GameRepository : RepositoryBase<Game, int>, IGameRepository
    {
        public GameRepository(DbContext context) : base(context)
        {
        }

        public async Task<Game> GetByIdIncludeAsync(int gameid)
        {
            return await _context.Set<Game>().
                Include(g => g.Platforms).
                Include(g => g.Genres).
                Where(g => g.Id == gameid).
                FirstOrDefaultAsync();
        }

        /*
        public async void AddGameGenreAsync(int gameid, int genreid)
        {
            var game = await GetByIdAsync(gameid);
            var genre = await _context.Set<Genre>().FindAsync(genreid);
            game.Genres.Add(genre);
        }

        public async void AddGamePlatformAsync(int gameid, int platformid)
        {
            var game = await GetByIdAsync(gameid);
            var platform = await _context.Set<Platform>().FindAsync(platformid);
            game.Platforms.Add(platform);
        }

        public async void RemoveGameGenreAsync(int gameid, int genreid)
        {
            var game = await GetByIdAsync(gameid);
            var genre = await _context.Set<Genre>().FindAsync(genreid);
            game.Genres.Remove(genre);
        }

        public async void RemoveGamePlatformAsync(int gameid, int platformid)
        {
            var game = await GetByIdAsync(gameid);
            var platform = await _context.Set<Platform>().FindAsync(platformid);
            game.Platforms.Remove(platform);
        }*/
    }
}

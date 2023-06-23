using DAL.Exceptions;
using DAL.Models;
using DAL.Repository.Contracts;
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
            var game = await _context.Set<Game>()
                                 .Include(g => g.Platforms)
                                 .Include(g => g.Genres)
                                 .Include(g => g.Reviews)
                                 .Where(g => g.Id == gameid)
                                 .FirstOrDefaultAsync();
            if (game is null)
            {
                throw new DatabaseNotFoundException();
            }
            return game;
        }
    }
}

using DAL.Exceptions;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class GameRepository : RepositoryBase<Game, int>, IGameRepository
    {
        public GameRepository(DbContext context) : base(context)
        {
        }
        public async Task<PagedList<Game>> GetAllIncludeAllAsync(GameParameters parameters)
        {
            var list = PagedList<Game>.ToPagedList(_context.Set<Game>()
                .Include(g => g.Platforms)
                .Include(g => g.Genres)
                .Include(g => g.Reviews), parameters.PageNumber, parameters.PageSize);

            if ((list is null) || (!list.Any()))
            {
                throw new DatabaseNotFoundException();
            }

            return list;
        }

        public async Task<Game> GetByIdIncludeAllAsync(int gameid)
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

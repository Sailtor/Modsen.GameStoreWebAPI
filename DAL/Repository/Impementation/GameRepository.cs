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

        public async Task<PagedList<Game>> GetAllFilteredAsync(GameParameters parameters)
        {
            var list = _context.Set<Game>()
                .Include(g => g.Platforms)
                .Include(g => g.Genres)
                .Include(g => g.Reviews)
                .AsQueryable();

            if (parameters.DeveloperId is not null)
            {
                list = list.Where(g => g.DeveloperId == parameters.DeveloperId);
            }
            if (parameters.MinReleaseDate is not null)
            {
                list = list.Where(g => g.ReleaseDate >= parameters.MinReleaseDate && g.ReleaseDate <= parameters.MaxReleaseDate);
            }
            if (parameters.MinPrice is not null && parameters.MinPrice is not null)
            {
                list = list.Where(g => g.Price >= parameters.MinPrice && g.Price <= parameters.MaxPrice);
            }

            //Not all advice worth listening to...
            /*if (parameters.MinScore is not null && parameters.MaxScore is not null)
            {
                list = list.Where(g => g.Score >= parameters.MinScore && g.Score <= parameters.MaxScore);
            }*/

            //If those two work, I'm fucking genius
            //Upd: THEY WORK, HAHAHHAHHHAHAHHAHHA, YEAAHHHHHHH!!!
            if (parameters.GenresIds is not null)
            {
                foreach (int genreid in parameters.GenresIds)
                {
                    list = list.Where(g => g.Genres.Any(g => g.Id == genreid));
                }
            }
            if (parameters.PlatformsIds is not null)
            {
                foreach (int platformid in parameters.PlatformsIds)
                {
                    list = list.Where(g => g.Platforms.Any(p => p.Id == platformid));
                }
            }

            var pagedList = PagedList<Game>.ToPagedList(list, parameters.PageNumber, parameters.PageSize);

            if ((pagedList is null) || (!pagedList.Any()))
            {
                throw new DatabaseNotFoundException();
            }
            return pagedList;
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

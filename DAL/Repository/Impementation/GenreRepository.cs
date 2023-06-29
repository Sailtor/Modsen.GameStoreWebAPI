using DAL.Exceptions;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class GenreRepository : RepositoryBase<Genre, int>, IGenreRepository
    {
        public GenreRepository(DbContext context) : base(context)
        {
        }

        public async Task<PagedList<Genre>> GetAllGenresAsync(GenreParameters parameters)
        {
            return await Task.Run(() =>
            {
                var genres = _context.Set<Genre>().AsQueryable();

                SearchByName(ref genres, parameters.SearchName);

                var pagedList = PagedList<Genre>.ToPagedList(genres.OrderBy(g => g.Name),
                    parameters.PageNumber,
                    parameters.PageSize);

                if ((pagedList is null) || (!pagedList.Any()))
                {
                    throw new DatabaseNotFoundException();
                }
                return pagedList;
            });
        }

        private void SearchByName(ref IQueryable<Genre> genres, string genreName)
        {
            if (string.IsNullOrWhiteSpace(genreName))
                return;
            genres = genres.Where(g => g.Name.ToLower().Contains(genreName.Trim().ToLower()));
        }
    }
}

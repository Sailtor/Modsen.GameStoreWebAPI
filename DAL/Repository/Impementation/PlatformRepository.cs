using DAL.Exceptions;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class PlatformRepository : RepositoryBase<Platform, int>, IPlatformRepository
    {
        public PlatformRepository(DbContext context) : base(context)
        {
        }

        public PagedList<Platform> GetAllPlatforms(PlatformParameters parameters)
        {
            var platforms = _context.Set<Platform>().AsQueryable();

            SearchByName(ref platforms, parameters.SearchName);

            var pagedList = PagedList<Platform>.ToPagedList(platforms.OrderBy(p => p.Name),
                parameters.PageNumber,
                parameters.PageSize);

            if ((pagedList is null) || (!pagedList.Any()))
            {
                throw new DatabaseNotFoundException();
            }
            return pagedList;
        }

        private void SearchByName(ref IQueryable<Platform> platforms, string platfName)
        {
            if (string.IsNullOrWhiteSpace(platfName))
                return;
            platforms = platforms.Where(p => p.Name.ToLower().Contains(platfName.Trim().ToLower()));
        }
    }
}

using DAL.Exceptions;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class DeveloperRepository : RepositoryBase<Developer, int>, IDeveloperRepository
    {
        public DeveloperRepository(DbContext context) : base(context)
        {
        }

        public PagedList<Developer> GetAllDevelopers(DeveloperParameters parameters)
        {
            var developers = _context.Set<Developer>().AsQueryable();

            SearchByName(ref developers, parameters.SearchName);

            var pagedList = PagedList<Developer>.ToPagedList(developers.OrderBy(d => d.Name),
                parameters.PageNumber,
                parameters.PageSize);

            if ((pagedList is null) || (!pagedList.Any()))
            {
                throw new DatabaseNotFoundException();
            }
            return pagedList;
        }

        private void SearchByName(ref IQueryable<Developer> developers, string devName)
        {
            if (string.IsNullOrWhiteSpace(devName))
                return;
            developers = developers.Where(d => d.Name.ToLower().Contains(devName.Trim().ToLower()));
        }
    }
}

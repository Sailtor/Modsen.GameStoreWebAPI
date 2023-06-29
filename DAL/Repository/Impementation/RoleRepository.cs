using DAL.Exceptions;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class RoleRepository : RepositoryBase<Role, int>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {

        }

        public async Task<PagedList<Role>> GetAllRolesAsync(RoleParameters parameters)
        {
            return await Task.Run(() =>
            {
                var roles = _context.Set<Role>().AsQueryable();

                SearchByName(ref roles, parameters.SearchName);

                var pagedList = PagedList<Role>.ToPagedList(roles.OrderBy(r => r.Name),
                    parameters.PageNumber,
                    parameters.PageSize);

                if ((pagedList is null) || (!pagedList.Any()))
                {
                    throw new DatabaseNotFoundException();
                }
                return pagedList;
            });
        }

        private void SearchByName(ref IQueryable<Role> roles, string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return;
            roles = roles.Where(r => r.Name.ToLower().Contains(roleName.Trim().ToLower()));
        }
    }
}
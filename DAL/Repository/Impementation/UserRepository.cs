using DAL.Exceptions;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repository.Impementation
{
    public class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
        public virtual async Task<PagedList<User>> GetAllFilteredAsync(UserParameters parameters)
        {
            var list = _context.Set<User>().
                AsQueryable();

            if (parameters.RoleId is not null)
            {
                list = list.Where(u => u.RoleId == parameters.RoleId);
            }

            var pagedList = PagedList<User>.ToPagedList(_context.Set<User>(), parameters.PageNumber, parameters.PageSize);
            if ((pagedList is null) || (!pagedList.Any()))
            {
                throw new DatabaseNotFoundException();
            }
            return pagedList;
        }
    }
}

using DAL.Exceptions;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {

        }

        public virtual async Task<PagedList<User>> GetAllAsync(UserParameters parameters)
        {
            return await Task.Run(() =>
            {
                var list = _context.Set<User>().
                AsQueryable();

                if (parameters.RoleId is not null)
                {
                    list = list.Where(u => u.RoleId == parameters.RoleId);
                }

                SearchByLogin(ref list, parameters.SearchLogin);
                SearchByEmail(ref list, parameters.SearchEmail);

                var pagedList = PagedList<User>.ToPagedList(list.OrderBy(u => u.Login), parameters.PageNumber, parameters.PageSize);

                if ((pagedList is null) || (!pagedList.Any()))
                {
                    throw new DatabaseNotFoundException();
                }

                return pagedList;
            });
        }

        private void SearchByLogin(ref IQueryable<User> users, string userLogin)
        {
            if (string.IsNullOrWhiteSpace(userLogin))
                return;
            users = users.Where(u => u.Login.ToLower().Contains(userLogin.Trim().ToLower()));
        }

        private void SearchByEmail(ref IQueryable<User> users, string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
                return;
            users = users.Where(g => g.Email.ToLower().Contains(userEmail.Trim().ToLower()));
        }
    }
}
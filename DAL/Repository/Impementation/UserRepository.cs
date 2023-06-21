using DAL.Models;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}

using DAL.Repository.Contracts;
using GameStoreWebAPI.Models;
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

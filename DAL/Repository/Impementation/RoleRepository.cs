using DAL.Repository.Contracts;
using GameStoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class RoleRepository : RepositoryBase<Role, int>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}

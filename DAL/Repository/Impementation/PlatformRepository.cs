using DAL.Repository.Contracts;
using GameStoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class PlatformRepository : RepositoryBase<Platform, int>, IPlatformRepository
    {
        public PlatformRepository(DbContext context) : base(context)
        {
        }
    }
}

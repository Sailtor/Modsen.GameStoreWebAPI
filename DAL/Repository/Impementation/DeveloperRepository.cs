using DAL.Repository.Contracts;
using GameStoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class DeveloperRepository : RepositoryBase<Developer, int>, IDeveloperRepository
    {
        public DeveloperRepository(DbContext context) : base(context)
        {
        }
    }
}

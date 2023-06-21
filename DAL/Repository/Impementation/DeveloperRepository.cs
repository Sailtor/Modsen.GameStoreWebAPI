using DAL.Models;
using DAL.Repository.Contracts;
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

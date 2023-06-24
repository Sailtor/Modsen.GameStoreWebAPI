using DAL.Models;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Impementation
{
    public class PurchaseRepository : RepositoryBase<Purchase,int>, IPurchaseRepository
    {
        public PurchaseRepository(DbContext context) : base(context)
        {
        }
    }
}

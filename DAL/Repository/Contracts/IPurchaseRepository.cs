using DAL.Models;
using DAL.Models.Query_String_Parameters;
using System.Linq.Expressions;

namespace DAL.Repository.Contracts
{
    public interface IPurchaseRepository : IRepository<Purchase, int>
    {
        Task<PagedList<Purchase>> GetAllFilteredAsync(PurchaseParameters parameters);
        Task<PagedList<Purchase>> FindFilteredAsync(Expression<Func<Purchase, bool>> predicate, PurchaseParameters parameters);
    }
}
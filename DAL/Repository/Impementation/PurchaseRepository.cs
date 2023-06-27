using DAL.Exceptions;
using DAL.Models;
using DAL.Models.Query_String_Parameters;
using DAL.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repository.Impementation
{
    public class PurchaseRepository : RepositoryBase<Purchase, int>, IPurchaseRepository
    {
        public PurchaseRepository(DbContext context) : base(context)
        {

        }
        public async Task<PagedList<Purchase>> GetAllFilteredAsync(PurchaseParameters parameters)
        {
            var list = _context.Set<Purchase>().AsQueryable();

            if (parameters.MinPurchaseDate is not null)
            {
                list = list.Where(g => g.PurchaseDate >= parameters.MinPurchaseDate && g.PurchaseDate <= parameters.MaxPurchaseDate);
            }

            var pagedList = PagedList<Purchase>.ToPagedList(list, parameters.PageNumber, parameters.PageSize);

            if ((pagedList is null) || (!pagedList.Any()))
            {
                throw new DatabaseNotFoundException();
            }
            return pagedList;
        }

        public async Task<PagedList<Purchase>> FindFilteredAsync(Expression<Func<Purchase, bool>> predicate, PurchaseParameters parameters)
        {
            var list = _context.Set<Purchase>()
                .Where(predicate)
                .AsQueryable();

            if (parameters.MinPurchaseDate is not null)
            {
                list = list.Where(g => g.PurchaseDate >= parameters.MinPurchaseDate && g.PurchaseDate <= parameters.MaxPurchaseDate);
            }

            var pagedList = PagedList<Purchase>.ToPagedList(list, parameters.PageNumber, parameters.PageSize);

            if ((pagedList is null) || (!pagedList.Any()))
            {
                throw new DatabaseNotFoundException();
            }
            return pagedList;
        }
    }
}

using DAL.Models;
using System.Linq.Expressions;

namespace DAL.Repository
{
    public interface IRepository<TEntity, TId>
        where TEntity : class
    {
        Task<TEntity> GetByIdAsync(TId entityid);
        Task<TEntity> GetByIdAsync(TId entityid, TId entityid2);
        Task<PagedList<TEntity>> GetAllAsync(QueryStringParameters parameters);
        Task<PagedList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, QueryStringParameters parameters);
        Task AddAsync(TEntity entity);
        Task Delete(TId entityid);
        Task Delete(TId entityid, TId entityid2);
    }
}
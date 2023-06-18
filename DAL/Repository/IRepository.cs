using System.Linq.Expressions;

namespace DAL.Repository
{
    internal interface IRepository<TEntity,TId> 
        where TEntity : class 
        where TId : class
    {
        Task<TEntity> GetAsync(TId id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity,bool>> predicate);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    }
}
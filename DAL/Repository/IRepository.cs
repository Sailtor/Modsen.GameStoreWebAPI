using System.Linq.Expressions;

namespace DAL.Repository
{
    public interface IRepository<TEntity,TId> 
        where TEntity : class 
    {
        Task<TEntity> GetByIdAsync(TId id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity,bool>> predicate);

        void AddAsync(TEntity entity);
        void AddRangeAsync(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
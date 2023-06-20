using System.Linq.Expressions;

namespace DAL.Repository
{
    public interface IRepository<TEntity,TId> 
        where TEntity : class 
    {
        Task<TEntity> GetByIdAsync(TId entityid);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity,bool>> predicate);

        Task AddAsync(TEntity entity);

        Task Delete(TId entityid);
    }
}
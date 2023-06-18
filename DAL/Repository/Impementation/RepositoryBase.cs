using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repository.Impementation
{
    public class RepositoryBase<TEntity,TId> : IRepository<TEntity,TId> where TEntity: class
    {
        protected readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }
        
        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async void AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async void AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
    }
}

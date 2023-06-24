using DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repository
{
    public class RepositoryBase<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        protected readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetByIdAsync(TId entityid)
        {
            var entity = await _context.Set<TEntity>().FindAsync(entityid);
            if (entity is null)
            {
                throw new DatabaseNotFoundException();
            }
            return entity;
        }

        public async Task<TEntity> GetByIdAsync(TId entityid, TId entityid2)
        {
            var entity = await _context.Set<TEntity>().FindAsync(entityid, entityid2);
            if (entity is null)
            {
                throw new DatabaseNotFoundException();
            }
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await _context.Set<TEntity>().Where(predicate).ToListAsync();
            if (entities is null)
            {
                throw new DatabaseNotFoundException();
            }
            return entities;
        }

        public async Task AddAsync(TEntity entity)
        {
            if (_context.Set<TEntity>().Contains(entity))
            {
                throw new EntityAlreadyExistsException();
            }
            await _context.Set<TEntity>().AddAsync(entity);
            
        }

        public async Task Delete(TId entityid)
        {
            TEntity? entity = await _context.Set<TEntity>().FindAsync(entityid);
            if (entity is null)
            {
                throw new DatabaseNotFoundException();
            }
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task Delete(TId entityid, TId entityid2)
        {
            TEntity? entity = await _context.Set<TEntity>().FindAsync(entityid, entityid2);
            if (entity is null)
            {
                throw new DatabaseNotFoundException();
            }
            _context.Set<TEntity>().Remove(entity);
        }
    }
}

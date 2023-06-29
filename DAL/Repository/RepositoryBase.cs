using DAL.Exceptions;
using DAL.Models;
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

        public virtual async Task<TEntity> GetByIdAsync(TId entityid)
        {
            var entity = await _context.Set<TEntity>().FindAsync(entityid);
            if (entity is null)
            {
                throw new DatabaseNotFoundException();
            }
            return entity;
        }

        public virtual async Task<TEntity> GetByIdAsync(TId entityid, TId entityid2)
        {
            var entity = await _context.Set<TEntity>().FindAsync(entityid, entityid2);
            if (entity is null)
            {
                throw new DatabaseNotFoundException();
            }
            return entity;
        }

        public virtual async Task<PagedList<TEntity>> GetAllAsync(QueryStringParameters parameters)
        {
            return await Task.Run(() =>
            {
                var list = PagedList<TEntity>.ToPagedList(_context.Set<TEntity>(), parameters.PageNumber, parameters.PageSize);
                if ((list is null) || (!list.Any()))
                {
                    throw new DatabaseNotFoundException();
                }
                return list;
            });
        }

        public virtual async Task<PagedList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, QueryStringParameters parameters)
        {
            return await Task.Run(() =>
            {
                var entities = PagedList<TEntity>.ToPagedList(_context.Set<TEntity>()
                .Where(predicate), parameters.PageNumber, parameters.PageSize);

                if ((entities is null) || (!entities.Any()))
                {
                    throw new DatabaseNotFoundException();
                }
                return entities;
            });
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task DeleteAsync(TId entityid)
        {
            TEntity? entity = await _context.Set<TEntity>().FindAsync(entityid);
            if (entity is null)
            {
                throw new DatabaseNotFoundException();
            }
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task DeleteAsync(TId entityid, TId entityid2)
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

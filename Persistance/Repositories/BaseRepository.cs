using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Persistance;
using Application.Extensions;

namespace Persistance.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly SRPDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(SRPDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity, bool overrideUser = true)
        {
            await _dbSet.AddAsync(entity);
            if (overrideUser)
                await _context.SaveChangesAsync();
            else
                await _context.SaveChangesWithoutUserAsync();
            return entity;
        }

        public async Task<int> AddManyAsync(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            var result = await _context.SaveChangesAsync();
            return result;
        }


        public async Task<int> RemoveManyAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateAsync(T entity, bool overrideUser = true)
        {
            _context.Update(entity);
            if (overrideUser)
                return await _context.SaveChangesAsync();
            return await _context.SaveChangesWithoutUserAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IList<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IList<T>> FindByAndOrderByAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderByItem)
        {
            return await _dbSet.Where(predicate).OrderBy(orderByItem).ToListAsync();
        }


        public async Task<T> FindByIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return await GetAllIncluding(includeProperties).FirstOrDefaultAsync(predicate);
        }

        public async Task<IList<T>> FindManyByIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return await GetAllIncluding(includeProperties).Where(predicate).ToListAsync();
        }
        public void Attach(T entity)
        {
            _dbSet.Attach(entity);
        }
        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public async Task<IList<T>> GetAllIncludeAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            return await GetAllIncluding(includeProperties).ToListAsync();
        }


        protected IQueryable<T> GetAllIncluding(Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = _dbSet;

            return includeProperties.Aggregate(queryable,
                (current, includeProperty) => current.Include(includeProperty));
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties);
        }

        public async Task<List<T>> GetPageAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, params Expression<Func<T, object>>[] includeProperties)
        {
            return await GetAllIncluding(includeProperties).Where(predicate).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public PaginatedList<T> GetPage2Async(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, params Expression<Func<T, object>>[] includeProperties)
        {
            var x = GetAllIncluding(includeProperties).Where(predicate).PaginatedList(pageNumber, pageSize);
            return x;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }
    }
}

using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistance
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<List<T>> GetPageAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, params Expression<Func<T, object>>[] includeProperties);
        PaginatedList<T> GetPage2Async(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity, bool overrideUser = true);
        Task<int> AddManyAsync(List<T> entities);
        Task<int> RemoveManyAsync(IEnumerable<T> entities);
        Task<int> UpdateAsync(T entity, bool overrideUser = true);
        Task DeleteAsync(T entity);
        Task<IList<T>> GetAllAsync();
        Task<IList<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<IList<T>> FindByAndOrderByAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderByItem);
        Task<T> FindByIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IList<T>> FindManyByIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<int> Save();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> Count(Expression<Func<T, bool>> predicate);
        Task<IList<T>> GetAllIncludeAsync(params Expression<Func<T, object>>[] includeProperties);
        void Attach(T entity);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        void UpdateAsync(T entity);
        void Delete(T entity);
        Task<T?> GetAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<int> SaveChangesAsync();
        Task<IEnumerable<T>> GetAllUntracked();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}

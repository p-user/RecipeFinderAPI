using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly ApiDbContext _context;
        public readonly DbSet<T> _entities;
        public BaseRepository(ApiDbContext apiDbContext)
        {
            _context = apiDbContext;
            _entities = apiDbContext.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            //if (entity is BaseEntity)
            //{
            //    var prop = entity.GetType().GetProperty("Status");
            //    prop.SetValue(entity, Status.Disabled);
            //    _context.Entry(entity).State = EntityState.Deleted;

            //}
            //else
                _entities.Remove(entity);

        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T?> GetAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void UpdateAsync(T entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async virtual Task<IEnumerable<T>> GetAllUntracked()
        {
            return await _entities.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public async Task<T> GetWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}

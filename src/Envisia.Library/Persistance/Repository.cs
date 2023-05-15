using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Envisia.Library.Persistance
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext Context { get; }
        private readonly DbSet<T> _entitiySet;

        public Repository(DbContext context)
        {
            Context = context;
            _entitiySet = Context.Set<T>();
        }

        public T Add(T entity)
        {
            EntityEntry<T> result = Context.Add(entity);

            return result.Entity;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            EntityEntry<T> result = await Context.AddAsync(entity);

            return result.Entity;
        }

        public void AddRange(IEnumerable<T> entities) => Context.AddRange(entities);

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) => await Context.AddRangeAsync(entities);

        public T Get(Expression<Func<T, bool>> expression) => _entitiySet.SingleOrDefault(expression);

        public IEnumerable<T> GetAll() => _entitiySet.AsEnumerable();

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression) => _entitiySet.Where(expression).AsEnumerable();

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) => await _entitiySet.ToListAsync(cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) => await _entitiySet.Where(expression).ToListAsync(cancellationToken);

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) => await _entitiySet.SingleOrDefaultAsync(expression);

        public IQueryable<T> GetQueryable() => _entitiySet;

        public void Remove(T entity) => Context.Remove(entity);

        public void RemoveRange(IEnumerable<T> entities) => Context.RemoveRange(entities);

        public T Update(T entity)
        {
            EntityEntry<T> result = Context.Update(entity);

            return result.Entity;
        }

        public void UpdateRange(IEnumerable<T> entities) => Context.UpdateRange(entities);
    }
}

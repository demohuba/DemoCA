using JEPCO.Application.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace JEPCO.Infrastructure.Persistence.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        #region Sync

        public TEntity GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public TEntity GetById(Guid id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public TEntity GetById(string id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);
        }
        public virtual void Update(TEntity entity)
        {
            if (context.Entry(entity).State != EntityState.Added)
            {
                context.Set<TEntity>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().UpdateRange(entities);
        }

        public virtual IQueryable<TEntity> GetAllQuerable(Expression<Func<TEntity, bool>> predicate = null)
        {
            return context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllByOrderAsync(Func<TEntity, object> predicate, string sort = "Ascending")
        {
            var query = sort == "Ascending" ?
            context.Set<TEntity>().OrderBy(predicate) :
            context.Set<TEntity>().OrderByDescending(predicate);

            return await Task.FromResult(query.ToList());
        }

        public void Remove(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
        }

        #endregion Sync

        #region Async

        public ValueTask<TEntity> GetByIdAsync(int id)
        {
            return context.Set<TEntity>().FindAsync(id);
        }

        public ValueTask<TEntity> GetByIdAsync(Guid id)
        {
            return context.Set<TEntity>().FindAsync(id);
        }

        public Task<TEntity> GetByIdNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate).AsNoTracking().FirstAsync();
        }

        public ValueTask<TEntity> GetByIdAsync(string id)
        {
            return context.Set<TEntity>().FindAsync(id);

        }
        public ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return context.Set<TEntity>().AddAsync(entity);
        }
        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return context.Set<TEntity>().AddRangeAsync(entities);
        }
        public Task<List<TEntity>> GetAllAsync()
        {
            return context.Set<TEntity>().ToListAsync();
        }
        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }

        #endregion Async
    }
}

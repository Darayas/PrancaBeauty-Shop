using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Infrastructure
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DbContext dbContext;
        public DbSet<TEntity> DbEntities { get; }

        public BaseRepository(DbContext _dbContext)
        {
            dbContext = _dbContext;
            DbEntities = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Get => DbEntities;

        public IQueryable<TEntity> GetNoTraking => DbEntities.AsNoTracking();

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool AutoSave = true)
        {
            await DbEntities.AddAsync(entity, cancellationToken);
            if (AutoSave == true)
                await dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> Entities, CancellationToken cancellationToken, bool AutoSave = true)
        {
            await this.DbEntities.AddRangeAsync(Entities, cancellationToken);
            if (AutoSave == true)
                await dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool AutoSave = true)
        {
            DbEntities.Remove(entity);
            if (AutoSave == true)
                await dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> Entities, CancellationToken cancellationToken, bool AutoSave = true)
        {
            DbEntities.RemoveRange(Entities);
            if (AutoSave == true)
                await dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetById(CancellationToken cancellationToken, params object[] Ids)
        {
            return await DbEntities.FindAsync(Ids, cancellationToken);
        }

        public virtual async Task<int> SaveChangeAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool AutoSave = true)
        {
            DbEntities.Update(entity);
            if (AutoSave)
                await dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> Entities, CancellationToken cancellationToken, bool AutoSave = true)
        {
            DbEntities.UpdateRange(Entities);
            if (AutoSave)
                await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

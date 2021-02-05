using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Domain
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        DbSet<TEntity> DbEntities { get; }

        IQueryable<TEntity> Get { get; }
        IQueryable<TEntity> GetNoTraking { get; }

        Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool AutoSave = true);
        Task AddRangeAsync(IEnumerable<TEntity> Entities, CancellationToken cancellationToken, bool AutoSave = true);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool AutoSave = true);
        Task DeleteRangeAsync(IEnumerable<TEntity> Entities, CancellationToken cancellationToken, bool AutoSave = true);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool AutoSave = true);
        Task UpdateRangeAsync(IEnumerable<TEntity> Entities, CancellationToken cancellationToken, bool AutoSave = true);

        Task<TEntity> GetById(CancellationToken cancellationToken, params object[] Ids);

        Task<int> SaveChangeAsync();
    }
}

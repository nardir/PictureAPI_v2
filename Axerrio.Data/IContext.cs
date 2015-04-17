using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Axerrio.Data.Entity
{
    public interface IContext : IDisposable
    {
        //int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IDbTransaction BeginTransaction();
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel);
        void UseTransaction(IDbTransaction transaction);

        IEntityRepository<TEntity> EntityRepository<TEntity>() where TEntity : class, IEntityState;

        IQueryable<TEntity> EntityQuery<TEntity>() where TEntity : class, IEntityState;
        IQueryable<TEntity> EntityQuery<TEntity>(string sql, params Object[] parameters) where TEntity : class, IEntityState;

        void AddEntity<TEntity>(TEntity entity) where TEntity : class, IEntityState;
        void UpdateEntity<TEntity>(TEntity entity) where TEntity : class, IEntityState;
        void RemoveEntity<TEntity>(TEntity entity) where TEntity : class, IEntityState;

        //int ExecuteSqlCommand(string sql, params Object[] parameters);
        Task<int> ExecuteSqlCommandAsync(string sql, params Object[] parameters);
        Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params Object[] parameters);
    }
}

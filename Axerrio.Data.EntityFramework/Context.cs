using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Axerrio.Data.Entity
{
    public abstract class Context<TContext> : DbContext, IContext 
        where TContext : DbContext 
    {
        private Dictionary<string, object> _sets;

        #region ctor

        static Context()
        {
            Database.SetInitializer<TContext>(null);
        }

        private void Configure()
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        protected Context(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configure();
        }

        protected Context(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            Configure();
        }

        #endregion ctor

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(builder);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntityState
        {
            if (_sets == null)
                _sets = new Dictionary<string, object>();

            var type = typeof(TEntity).Name;

            if (_sets.ContainsKey(type))
                return (DbSet<TEntity>)_sets[type];

            _sets.Add(type, base.Set<TEntity>());

            return (DbSet<TEntity>)_sets[type];
        }

        #region EntityRepository

        public virtual IEntityRepository<TEntity> EntityRepository<TEntity>() where TEntity : class, IEntityState
        {
            return new EntityRepository<TEntity>(this);
        }

        public virtual IQueryable<TEntity> EntityQuery<TEntity>() where TEntity : class, IEntityState
        {
            return Set<TEntity>().AsNoTracking();
        }

        public virtual IQueryable<TEntity> EntityQuery<TEntity>(string sql, params object[] parameters) where TEntity : class, IEntityState
        {
            return Set<TEntity>().SqlQuery(sql, parameters).AsNoTracking().AsQueryable();
        }

        public virtual void AddEntity<TEntity>(TEntity entity) where TEntity : class, IEntityState
        {
            Set<TEntity>().Attach(entity);
        }

        public virtual void UpdateEntity<TEntity>(TEntity entity) where TEntity : class, IEntityState
        {
            Set<TEntity>().Attach(entity);
        }

        public virtual void RemoveEntity<TEntity>(TEntity entity) where TEntity : class, IEntityState
        {
            Set<TEntity>().Attach(entity);
        }

        #endregion EntityRepository

        #region UoW

        private void ApplyEntityState()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries<IEntityState>())
            {
                dbEntityEntry.State = dbEntityEntry.Entity.State.ConvertToEFEntityState();
            }
        }

        private void AdjustEntityState()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries<IEntityState>())
            {
                IEntityState entity = dbEntityEntry.Entity;

                entity.State = dbEntityEntry.State.ConvertFromEFEntityState(entity.State);

                dbEntityEntry.State = System.Data.Entity.EntityState.Detached;
            }
        }

        public new int SaveChanges()
        {
            return SaveChangesAsync(null).Result;
        }

        private async Task<int> SaveChangesAsync(CancellationToken? cancellationToken)
        {
            int changes;

            ApplyEntityState();

            try
            {
                if (cancellationToken != null)
                    changes = await base.SaveChangesAsync((CancellationToken)cancellationToken);
                else
                    changes = await base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex.ConvertToContextValidationException();
            }
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    throw new Exception("", ex);
            //}
            catch (DbUpdateException ex)
            {
                throw new DataException(ex.ToString());
            }

            AdjustEntityState();

            return changes;
        }

        public new Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return SaveChangesAsync((CancellationToken?)cancellationToken);
        }

        public new Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(null);
        }

        #endregion UoW

        #region Transaction

        public IDbTransaction BeginTransaction()
        {
            return Database.BeginTransaction().UnderlyingTransaction;
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return Database.BeginTransaction(isolationLevel).UnderlyingTransaction;
        }

        public void UseTransaction(IDbTransaction transaction)
        {
            Database.UseTransaction((DbTransaction)transaction);
        }

        #endregion Transaction

        #region RawSQL

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sql, parameters);
        }

        private Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken? cancellationToken, params Object[] parameters)
        {
            if (cancellationToken != null)
                return Database.ExecuteSqlCommandAsync(sql, (CancellationToken)cancellationToken, parameters);
            
            return Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return ExecuteSqlCommandAsync(sql, null, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters)
        {
            return ExecuteSqlCommandAsync(sql, (CancellationToken?)cancellationToken, parameters);
        }

        #endregion RawSQL
    }
}
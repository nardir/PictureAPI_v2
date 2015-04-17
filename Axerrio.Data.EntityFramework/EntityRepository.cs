using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axerrio.Data.Entity
{
    public class EntityRepository<TEntity> :  IEntityRepository<TEntity> where TEntity : class, IEntityState
    {
        private readonly IContext _context;

        public EntityRepository(IContext context)
        {
            _context = context;
        }

        public virtual IQueryable<TEntity> Query()
        {
            return _context.EntityQuery<TEntity>();
        }

        public virtual IQueryable<TEntity> Query(string sql, params object[] parameters)
        {
            return _context.EntityQuery<TEntity>(sql, parameters);
        }

        public virtual void AddEntity(TEntity entity)
        {
            _context.AddEntity<TEntity>(entity);
        }

        public virtual void UpdateEntity(TEntity entity)
        {
            _context.UpdateEntity<TEntity>(entity);
        }

        public virtual void RemoveEntity(TEntity entity)
        {
            _context.RemoveEntity<TEntity>(entity);
        }
    }
}

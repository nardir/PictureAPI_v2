using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axerrio.Data.Entity
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntityState
    {
        IQueryable<TEntity> Query();
        IQueryable<TEntity> Query(string sql, params Object[] parameters);

        void AddEntity(TEntity entity);
        void UpdateEntity(TEntity entity);
        void RemoveEntity(TEntity entity);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Axerrio.Data.Entity;
using System.Data.Entity;

namespace Axerrio.Data.Entity
{
    public static class EntityStateExtensions
    {
        public static System.Data.Entity.EntityState ConvertToEFEntityState(this Axerrio.Data.Entity.EntityState state)
        {
            switch (state)
            {
                case Axerrio.Data.Entity.EntityState.Added:
                    return System.Data.Entity.EntityState.Added;

                case Axerrio.Data.Entity.EntityState.Modified:
                    return System.Data.Entity.EntityState.Modified;

                case Axerrio.Data.Entity.EntityState.Deleted:
                    return System.Data.Entity.EntityState.Deleted;

                default:
                    return System.Data.Entity.EntityState.Unchanged;
            }
        }

        public static Axerrio.Data.Entity.EntityState ConvertFromEFEntityState(this System.Data.Entity.EntityState state, Axerrio.Data.Entity.EntityState currentState)
        {
            switch (state)
            {
                case System.Data.Entity.EntityState.Unchanged:
                    return Axerrio.Data.Entity.EntityState.Unchanged;

                case System.Data.Entity.EntityState.Added:
                    return Axerrio.Data.Entity.EntityState.Added;

                case System.Data.Entity.EntityState.Modified:
                    return Axerrio.Data.Entity.EntityState.Modified;

                case System.Data.Entity.EntityState.Deleted:
                    return Axerrio.Data.Entity.EntityState.Deleted;

                default:
                    return currentState;
            }
        }
    }
}

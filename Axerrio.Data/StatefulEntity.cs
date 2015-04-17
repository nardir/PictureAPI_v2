using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axerrio.Data.Entity
{
    public abstract class StatefulEntity : IEntityState
    {
        [NotMapped]
        public EntityState State { get; set; }

        protected StatefulEntity()
        {
            State = EntityState.Unchanged;
        }
    }
}

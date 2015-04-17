using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axerrio.Data.Entity
{
    public interface IEntityState
    {
        [NotMapped]
        EntityState State { get; set; }
    }
}

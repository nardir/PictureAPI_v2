using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axerrio.Data.Entity
{
    public static class DataConfiguration
    {
        public static void Configure(Action configurationCallback)
        {
            configurationCallback();
        }
    }
}

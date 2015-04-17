using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Axerrio.Data.Entity
{
    public class ContextDbConfiguration: DbConfiguration
    {
        static ContextDbConfiguration()
        {
            ExecutionStrategy = new DefaultExecutionStrategy();
        }

        public ContextDbConfiguration()
        {
            SetExecutionStrategy(SqlProviderServices.ProviderInvariantName, () => ExecutionStrategySuspended ? new DefaultExecutionStrategy() : ExecutionStrategy);
        }

        public static bool ExecutionStrategySuspended
        {
            get
            {
                return (bool?)CallContext.LogicalGetData("ExecutionStrategySuspended") ?? false;
            }
            set
            {
                CallContext.LogicalSetData("ExecutionStrategySuspended", value);
            }
        }

        public static void SuspendExecutionStrategy()
        {
            ExecutionStrategySuspended = true;
        }

        public static void ResumeExecutionStrategy()
        {
            ExecutionStrategySuspended = false;
        }

        public static IDbExecutionStrategy ExecutionStrategy{ get; private set; }

        public static void SetExecutionStrategy(IDbExecutionStrategy executionStrategy)
        {
            ExecutionStrategy = executionStrategy;
        }
    }
}

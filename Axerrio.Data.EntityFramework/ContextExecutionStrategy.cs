using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Axerrio.Data.Entity
{
    public class ContextExecutionStrategy: IContextExecutionStrategy
    {
        public void SuspendExecutionStrategy()
        {
            ContextDbConfiguration.SuspendExecutionStrategy();
        }

        public void ResumeExecutionStrategy()
        {
            ContextDbConfiguration.ResumeExecutionStrategy();
        }

        protected virtual async Task ExecuteOnExecutionStrategyAsync(Func<Task> operation, CancellationToken? cancellationToken)
        {
            IDbExecutionStrategy strategy = ContextDbConfiguration.ExecutionStrategy;

            SuspendExecutionStrategy();

            await strategy.ExecuteAsync(operation, cancellationToken ?? new CancellationToken());

            ResumeExecutionStrategy();
        }

        public virtual Task ExecuteOnExecutionStrategyAsync(Func<Task> operation)
        {
            return ExecuteOnExecutionStrategyAsync(operation, null);
        }

        public virtual Task ExecuteOnExecutionStrategyAsync(Func<Task> operation, CancellationToken cancellationToken)
        {
            return ExecuteOnExecutionStrategyAsync(operation, (CancellationToken?)cancellationToken);
        }

        protected virtual async Task<TResult> ExecuteOnExecutionStrategyAsync<TResult>(Func<Task<TResult>> operation, CancellationToken? cancellationToken)
        {
            IDbExecutionStrategy strategy = ContextDbConfiguration.ExecutionStrategy;

            SuspendExecutionStrategy();

            var result = await strategy.ExecuteAsync(operation, cancellationToken ?? new CancellationToken());

            ResumeExecutionStrategy();

            return result;
        }

        public Task<TResult> ExecuteOnExecutionStrategyAsync<TResult>(Func<Task<TResult>> operation)
        {
            return ExecuteOnExecutionStrategyAsync(operation, null);
        }

        public Task<TResult> ExecuteOnExecutionStrategyAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken)
        {
            return ExecuteOnExecutionStrategyAsync(operation, (CancellationToken?)cancellationToken);
        }
    }
}

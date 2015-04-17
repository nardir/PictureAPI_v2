using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Axerrio.Data.Entity
{
    public interface IContextExecutionStrategy
    {
        void SuspendExecutionStrategy();
        void ResumeExecutionStrategy();
        Task ExecuteOnExecutionStrategyAsync(Func<Task> operation);
        Task ExecuteOnExecutionStrategyAsync(Func<Task> operation, CancellationToken cancellationToken);
        Task<TResult> ExecuteOnExecutionStrategyAsync<TResult>(Func<Task<TResult>> operation);
        Task<TResult> ExecuteOnExecutionStrategyAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken);
    }
}
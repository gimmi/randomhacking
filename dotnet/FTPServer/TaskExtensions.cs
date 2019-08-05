using System.Threading;
using System.Threading.Tasks;

namespace FTPServer
{
    public static class TaskExtensions
    {
        public static async Task<TResult> DefaultIfCanceledAsync<TResult>(this Task<TResult> task, CancellationToken ct)
        {
            var cancelTask = Task.Delay(Timeout.Infinite, ct);
            var resultTask = await Task.WhenAny(task, cancelTask);
            if (ReferenceEquals(cancelTask, resultTask))
            {
                return default;
            }

            return await task;
        }
    }
}
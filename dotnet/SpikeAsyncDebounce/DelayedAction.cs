using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpikeAsyncDebounce
{
    public class DelayedAction : IDisposable
    {
        private readonly TimeSpan _interval;
        private readonly Action _action;

        private CancellationTokenSource _cts = new CancellationTokenSource();
        private Task _invokeTask = Task.FromCanceled(new CancellationToken(true));

        public DelayedAction(TimeSpan interval, Action action)
        {
            _interval = interval;
            _action = action;
        }

        public bool HasBeenInvoked => _invokeTask.Status == TaskStatus.RanToCompletion || _invokeTask.Status == TaskStatus.Faulted;

        public void Reset()
        {
            var ct = ReplaceCts();
            _invokeTask = Task.Delay(_interval, ct)
                .ContinueWith(_ => _action(), ct, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Current);
        }

        public void Invoke()
        {
            var ct = ReplaceCts();
            _invokeTask = Task.Run(_action, ct);
        }

        public void Cancel() => _cts.Cancel();

        public void Dispose()
        {
            var oldCts = Interlocked.Exchange(ref _cts, null);
            oldCts.Cancel();
            oldCts.Dispose();
        }

        private CancellationToken ReplaceCts()
        {
            var newCts = new CancellationTokenSource();
            var newCt = newCts.Token;
            var oldCts = Interlocked.Exchange(ref _cts, newCts);
            oldCts.Cancel();
            oldCts.Dispose();
            return newCt;
        }
    }
}

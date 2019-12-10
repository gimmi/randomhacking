using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpikeAsyncDebounce
{
    public class DebouncedAction : IDisposable
    {
        private readonly TimeSpan _interval;
        private readonly Action _action;

        private CancellationTokenSource _cts = new CancellationTokenSource();

        public DebouncedAction(TimeSpan interval, Action action)
        {
            _interval = interval;
            _action = action;
        }

        public void Invoke()
        {
            var newCts = new CancellationTokenSource();
            var newCt = newCts.Token;
            var oldCts = Interlocked.Exchange(ref _cts, newCts);
            oldCts.Cancel();
            oldCts.Dispose();
            Task.Delay(_interval, newCt)
                .ContinueWith(_ => _action(), newCt, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Current);
        }

        public void Dispose()
        {
            var oldCts = Interlocked.Exchange(ref _cts, null);
            oldCts.Cancel();
            oldCts.Dispose();
        }
    }
}

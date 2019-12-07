using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpikeAsyncDebounce
{
    public class DebouncedAction : IDisposable
    {
        private readonly TimeSpan _interval;
        private readonly Action _action;

        private CancellationTokenSource _cts;

        public DebouncedAction(TimeSpan interval, Action action)
        {
            _interval = interval;
            _action = action;
        }

        public void Invoke()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            Task.Delay(_interval, _cts.Token)
                .ContinueWith(_ => _action(), _cts.Token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Current);
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}

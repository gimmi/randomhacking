﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpikeAsyncDebounce
{
    public class DelayedAction : IDisposable
    {
        private readonly TimeSpan _interval;
        private readonly Action _action;

        private CancellationTokenSource _cts = new CancellationTokenSource();
        private Task _actionTask = Task.FromCanceled(new CancellationToken(true));

        public DelayedAction(TimeSpan interval, Action action)
        {
            _interval = interval;
            _action = action;
        }

        public bool HasBeenInvoked => _actionTask.Status == TaskStatus.RanToCompletion || _actionTask.Status == TaskStatus.Faulted;

        public void Reset()
        {
            var newCts = new CancellationTokenSource();
            var newCt = newCts.Token;
            var oldCts = Interlocked.Exchange(ref _cts, newCts);
            oldCts.Cancel();
            oldCts.Dispose();
            _actionTask = Task.Delay(_interval, newCt)
                .ContinueWith(_ => _action(), newCt, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Current);
        }

        public void Cancel() => _cts.Cancel();

        public void Dispose()
        {
            var oldCts = Interlocked.Exchange(ref _cts, null);
            oldCts.Cancel();
            oldCts.Dispose();
        }
    }
}

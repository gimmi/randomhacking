using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectionStateMachine
{
    public class ConnectionStateMachine : IDisposable
    {
        private readonly SemaphoreSlim _mutex = new SemaphoreSlim(1, 1);
        private CancellationTokenSource _currentCts = new CancellationTokenSource();
        private ConnectionStates _currentState = ConnectionStates.Disconnected;

        public async Task ChangeAsync(ConnectionStates state)
        {
            await _mutex.WaitAsync();
            try
            {
                if (_currentState != state)
                {
                    var previousCts = _currentCts;

                    _currentState = state;
                    _currentCts = new CancellationTokenSource();

                    previousCts.Cancel();
                    previousCts.Dispose();
                }
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task WaitForStateAsync(ConnectionStates state, int millisecondsDelat)
        {
            using var cts = new CancellationTokenSource(millisecondsDelat);
            await WaitForStateAsync(state, cts.Token);
        }

        public async Task WaitForStateAsync(ConnectionStates state, TimeSpan timeout)
        {
            using var cts = new CancellationTokenSource(timeout);
            await WaitForStateAsync(state, cts.Token);
        }

        public async Task WaitForStateAsync(ConnectionStates state, CancellationToken cancellationToken)
        {
            // Early check, for performances
            cancellationToken.ThrowIfCancellationRequested();
            if (_currentState == state)
            {
                return;
            }

            var (currentState, currentCt) = await AtomicGetAsync(cancellationToken);
            while (currentState != state)
            {
                var tcs = new TaskCompletionSource<object>();
                await using var ctr1 = currentCt.Register(() => tcs.TrySetResult(null));
                await using var ctr2 = cancellationToken.Register(() => tcs.TrySetCanceled(cancellationToken));
                await tcs.Task;
                (currentState, currentCt) = await AtomicGetAsync(cancellationToken);
            }
        }

        public void Dispose()
        {
            _currentCts.Dispose();
        }

        private async Task<(ConnectionStates, CancellationToken)> AtomicGetAsync(CancellationToken cancellationToken)
        {
            await _mutex.WaitAsync(cancellationToken);
            try
            {
                return (_currentState, _currentCts.Token);
            }
            finally
            {
                _mutex.Release();
            }
        }
    }
}

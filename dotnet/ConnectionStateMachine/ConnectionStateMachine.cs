using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectionStateMachine
{
    public class ConnectionStateMachine : IDisposable
    {
        private readonly SemaphoreSlim _mutex = new SemaphoreSlim(1, 1);
        private CancellationTokenSource _changeCts = new CancellationTokenSource();
        private ConnectionStates _currentState = ConnectionStates.Disconnected;

        public async Task ChangeAsync(ConnectionStates state)
        {
            await _mutex.WaitAsync();
            try
            {
                if (_currentState != state)
                {
                    _currentState = state;
                    _changeCts.Cancel();
                    _changeCts.Dispose();
                    _changeCts = new CancellationTokenSource();
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

            var (currentState, changeCt) = await AtomicGetAsync(cancellationToken);
            while (currentState != state)
            {
                var tcs = new TaskCompletionSource<object>();
                await using var ctr1 = changeCt.Register(() => tcs.TrySetResult(null));
                await using var ctr2 = cancellationToken.Register(() => tcs.TrySetCanceled(cancellationToken));
                await tcs.Task;
                (currentState, changeCt) = await AtomicGetAsync(cancellationToken);
            }
        }

        public void Dispose()
        {
            _changeCts.Dispose();
        }

        private async Task<(ConnectionStates, CancellationToken)> AtomicGetAsync(CancellationToken cancellationToken)
        {
            await _mutex.WaitAsync(cancellationToken);
            try
            {
                return (_currentState, _changeCts.Token);
            }
            finally
            {
                _mutex.Release();
            }
        }
    }
}

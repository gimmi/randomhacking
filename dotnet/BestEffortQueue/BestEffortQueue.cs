using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BestEffortQueue
{
    public class BestEffortQueue<T>
    {
        private readonly int _approxCapacity;
        private readonly ConcurrentQueue<T> _concurrentQueue;
        private readonly SemaphoreSlim _semaphoreSlim;

        private T _current;
        private int _lossCount;

        public BestEffortQueue(int approxCapacity)
        {
            _approxCapacity = approxCapacity;
            _semaphoreSlim = new SemaphoreSlim(0);
            _concurrentQueue = new ConcurrentQueue<T>();
            _current = default;
            _lossCount = 0;
        }

        public T Current => _current;
        public int LossCount => _lossCount;

        public bool TryEnqueue(T item)
        {
            if (_semaphoreSlim.CurrentCount >= _approxCapacity)
            {
                Interlocked.Increment(ref _lossCount);
                return false;
            }

            _concurrentQueue.Enqueue(item);
            _semaphoreSlim.Release();

            return true;
        }

        public async Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _semaphoreSlim.WaitAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return false;
            }
            return _concurrentQueue.TryDequeue(out _current);
        }
    }
}
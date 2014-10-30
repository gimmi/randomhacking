using System;
using System.Collections.Concurrent;
using System.Threading;

namespace SpikeBgWorker
{
	public class Al : IDisposable
	{
		private readonly ConsumerThread<byte[]> _consumerThread;

		public Al()
		{
			_consumerThread = new ConsumerThread<byte[]>(Consume);
		}

		private bool Consume(byte[] item, bool disposing)
		{
			if (disposing)
			{
				
			}
			else
			{
				
			}
			return true;
		}

		public void Enqueue(byte[] item)
		{
			_consumerThread.Enqueue(item);
		}

		public void Dispose()
		{
			_consumerThread.Dispose();
		}
	}

	public class ConsumerThread<T> : IDisposable
	{
		private readonly ConsumeDelegate _consume;
		private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
		private readonly AutoResetEvent _event = new AutoResetEvent(false);
		private readonly Thread _thread;
		private volatile bool _disposed;

		public delegate bool ConsumeDelegate(T item, bool disposing);

		public ConsumerThread(ConsumeDelegate consume)
		{
			_consume = consume;
			_disposed = false;
			_thread = new Thread(Start) { Name = GetType().FullName };
			_thread.Start();
		}

		private void Start()
		{
			while (true)
			{
				_event.WaitOne();
				T item;
				if (_queue.TryPeek(out item) && _consume.Invoke(item, _disposed))
				{
					_queue.TryDequeue(out item);
				}
				if (_queue.TryPeek(out item))
				{
					_event.Set();
				}
				else if(_disposed)
				{
					break;
				}
			}
		}

		public void Enqueue(T item)
		{
			if (_disposed)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			_queue.Enqueue(item);
			_event.Set();
		}

		public void Dispose()
		{
			_disposed = true;
			_event.Set();
			_thread.Join();
		}
	}
}
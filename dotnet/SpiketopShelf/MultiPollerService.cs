using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using log4net;

namespace SpiketopShelf
{
	public class MultiPollerService : IDisposable
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof (MultiPollerService));
		private readonly TimeSpan _reinitWait;
		private readonly ThreadController _threadController;
		private readonly Func<IService>[] _serviceFactories;
		private readonly Thread[] _threads;

		public MultiPollerService(IEnumerable<Func<IService>> serviceFactories, TimeSpan reinitWait)
		{
			_reinitWait = reinitWait;
			_serviceFactories = serviceFactories.ToArray();
			_threads = new Thread[_serviceFactories.Length];
			_threadController = new ThreadController();
		}

		public void Dispose()
		{
			Stop();
		}

		public void Start()
		{
			if (_threadController.IsRunning)
			{
				return;
			}
			_threadController.Run();
			for (var i = 0; i <_serviceFactories.Length; i++)
			{
				var serviceFactory = _serviceFactories[i];
				_threads[i] = new Thread(() => ThreadStart(serviceFactory));
				_threads[i].Start();
			}
		}

		private void ThreadStart(Func<IService> serviceFactory)
		{
			while (_threadController.IsRunning)
			{
				try
				{
					var service = serviceFactory.Invoke();
					while (_threadController.IsRunning)
					{
						var nextIterationDelay = service.RunIteration();
						_threadController.WaitWhileRunning(nextIterationDelay);
					}
				}
				catch (Exception ex)
				{
					Log.Fatal(string.Concat("Unhandled exception on worker thread, will restart thread in ", _reinitWait), ex);
					_threadController.WaitWhileRunning(_reinitWait);
				}
			}
		}

		public void Stop()
		{
			if (_threadController.IsStopped)
			{
				return;
			}
			_threadController.Stop();
			foreach (Thread thread in _threads)
			{
				thread.Join();
			}
		}

		public class ThreadController
		{
			private readonly ManualResetEvent _stopEvent = new ManualResetEvent(true);

			public bool IsRunning
			{
				get { return !_stopEvent.WaitOne(0); }
			}

			public bool IsStopped
			{
				get { return !IsRunning; }
			}

			public void Run()
			{
				_stopEvent.Reset();
			}

			public void Stop()
			{
				_stopEvent.Set();
			}

			public void WaitWhileRunning(TimeSpan timeSpan)
			{
				_stopEvent.WaitOne(timeSpan);
			}
		}
	}
}
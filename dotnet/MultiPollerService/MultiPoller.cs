using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac;
using log4net;

namespace MultiPollerService
{
	public class MultiPoller
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof (MultiPoller));
		private readonly IContainer _container;
		private readonly string[] _pollerNames;
		private readonly TimeSpan _reinitWait;
		private readonly ThreadController _threadController;
		private readonly Thread[] _threads;

		public MultiPoller(IContainer container, IEnumerable<string> pollerNames, TimeSpan reinitWait)
		{
			_container = container;
			_pollerNames = pollerNames.ToArray();
			_reinitWait = reinitWait;
			_threads = new Thread[_pollerNames.Length];
			_threadController = new ThreadController();
		}

		public void Start()
		{
			if (_threadController.IsRunning)
			{
				return;
			}
			_threadController.Run();
			for (var i = 0; i < _pollerNames.Length; i++)
			{
				_threads[i] = new Thread(ThreadStart) {Name = _pollerNames[i]};
				_threads[i].Start(_pollerNames[i]);
			}
		}

		private void ThreadStart(object pollerName)
		{
			while (_threadController.IsRunning)
			{
				try
				{
					using (var scope = _container.BeginLifetimeScope())
					{
						var poller = scope.ResolveNamed<IPoller>(pollerName.ToString());
						var nextIterationDelay = poller.Poll();
						_threadController.WaitWhileRunning(nextIterationDelay);
					}
				}
				catch (Exception ex)
				{
					Log.Fatal(string.Concat("Unhandled exception on poller thread, will wait for ", _reinitWait, " then restart polling."), ex);
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
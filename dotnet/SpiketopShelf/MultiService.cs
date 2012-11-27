using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using log4net;

namespace SpiketopShelf
{
	public class MultiService
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof (MultiService));
		private readonly TimeSpan _reinitWait;
		private readonly IServiceFactory[] _serviceFactories;
		private readonly Thread[] _threads;
		private volatile bool _running;

		public MultiService(IEnumerable<IServiceFactory> serviceFactories, TimeSpan reinitWait)
		{
			_reinitWait = reinitWait;
			_serviceFactories = serviceFactories.ToArray();
			_threads = new Thread[_serviceFactories.Length];
		}

		public void Start()
		{
			if (_running)
			{
				throw new InvalidOperationException("Already started.");
			}
			_running = true;
			foreach (int i in Enumerable.Range(0, _serviceFactories.Length))
			{
				_threads[i] = new Thread(ThreadStart);
				_threads[i].Start(_serviceFactories[i]);
			}
		}

		private void ThreadStart(object o)
		{
			var serviceInstance = (IServiceFactory) o;
			while (_running)
			{
				try
				{
					ThreadContext.Properties.Clear();
					IService service = serviceInstance.Create();
					while (_running)
					{
						service.RunIteration();
					}
				}
				catch (Exception ex)
				{
					Log.Fatal(string.Concat("Unhandled exception on worker thread, will restart thread in ", _reinitWait), ex);
					Wait(_reinitWait);
				}
			}
		}

		private void Wait(TimeSpan timeSpan)
		{
			while (timeSpan > TimeSpan.Zero && _running)
			{
				TimeSpan sleepTimeSpan = TimeSpan.FromSeconds(1);
				Thread.Sleep(sleepTimeSpan);
				timeSpan -= sleepTimeSpan;
			}
		}

		public void Stop()
		{
			_running = false;
			foreach (Thread thread in _threads)
			{
				thread.Join();
			}
		}
	}
}
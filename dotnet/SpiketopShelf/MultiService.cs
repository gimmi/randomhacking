using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using log4net;

namespace SpiketopShelf
{
	public class MultiService : IDisposable
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof (MultiService));
		private readonly TimeSpan _reinitWait;
		private readonly RunController _runController;
		private readonly IServiceFactory[] _serviceFactories;
		private readonly Thread[] _threads;

		public MultiService(IEnumerable<IServiceFactory> serviceFactories, TimeSpan reinitWait)
		{
			_reinitWait = reinitWait;
			_serviceFactories = serviceFactories.ToArray();
			_threads = new Thread[_serviceFactories.Length];
			_runController = new RunController();
		}


		public void Dispose()
		{
			Stop();
		}

		public void Start()
		{
			if (_runController.Stopped)
			{
				_runController.Run();
				foreach (int i in Enumerable.Range(0, _serviceFactories.Length))
				{
					_threads[i] = new Thread(ThreadStart);
					_threads[i].Start(_serviceFactories[i]);
				}
			}
		}

		private void ThreadStart(object o)
		{
			var serviceInstance = (IServiceFactory) o;
			while (_runController.Running)
			{
				try
				{
					ThreadContext.Properties.Clear();
					IService service = serviceInstance.Create();
					while (_runController.Running)
					{
						_runController.WaitWhileRunning(service.RunIteration());
					}
				}
				catch (Exception ex)
				{
					Log.Fatal(string.Concat("Unhandled exception on worker thread, will restart thread in ", _reinitWait), ex);
					_runController.WaitWhileRunning(_reinitWait);
				}
			}
		}

		public void Stop()
		{
			if (_runController.Running)
			{
				_runController.Stop();
				foreach (Thread thread in _threads)
				{
					thread.Join();
				}
			}
		}
	}
}
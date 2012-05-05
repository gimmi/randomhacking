using System;
using log4net;

namespace SpikeWindsor3
{
	public class DisposableService : IDisposableService, IDisposable
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(DisposableService));
		private static int _instanceCounter;
		public bool Disposed;
		private readonly int _instanceId;

		public DisposableService()
		{
			_instanceId = ++_instanceCounter;
			Log.DebugFormat("{0} #{1} created", typeof(DisposableService), _instanceId);
		}

		public void Dispose()
		{
			Disposed = true;
			Log.DebugFormat("{0} #{1} disposed", typeof(DisposableService), _instanceId);
		}
	}
}
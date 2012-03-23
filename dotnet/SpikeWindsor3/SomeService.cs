using System;

namespace SpikeWindsor3
{
	public class SomeService : ISomeService, IDisposable
	{
		public bool Disposed;

		public void Dispose()
		{
			Disposed = true;
		}
	}
}
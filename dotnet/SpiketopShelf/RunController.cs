using System;
using System.Threading;

namespace SpiketopShelf
{
	public class RunController
	{
		private readonly ManualResetEvent _stopEvent = new ManualResetEvent(true);

		public bool Running
		{
			get { return !_stopEvent.WaitOne(0); }
		}

		public bool Stopped
		{
			get { return !Running; }
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
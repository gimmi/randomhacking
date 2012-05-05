using log4net;

namespace SpikeWindsor3
{
	public class Service : IService
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Service));
		private static int _instanceCounter;

		public Service(IDisposableService disposableService)
		{
			Log.DebugFormat("{0} #{1} created", typeof(Service), ++_instanceCounter);
		}
	}
}
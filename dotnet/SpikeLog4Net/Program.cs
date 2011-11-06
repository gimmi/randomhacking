using log4net;
using log4net.Config;

namespace SpikeLog4Net
{
	public class Program
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

		private static void Main()
		{
			XmlConfigurator.Configure();

			Log.Debug("debug");
			Log.Info("info");
			Log.Warn("warn");
			Log.Error("error");
			Log.Fatal("fatal");
		}
	}
}
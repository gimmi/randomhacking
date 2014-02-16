using System;
using System.Threading;
using Topshelf;
using log4net;
using log4net.Config;

namespace SpiketopShelf
{
	public class Program
	{
		public static void Main()
		{
			BasicConfigurator.Configure();

			var multiService = new MultiPollerService(new Func<IService>[] {
				() => new Service("svc1"),
				() => new Service("svc2"),
				() => new Service("svc3")
			}, TimeSpan.FromSeconds(5));

			HostFactory.Run(hcfg => {
				hcfg.UseLog4Net();
				hcfg.Service<MultiPollerService>(scfg => {
					scfg.ConstructUsing(() => multiService);
					scfg.WhenStarted(svc => svc.Start());
					scfg.WhenStopped(svc => svc.Stop());
				});
			});
		}
	}

	public class Service : IService
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof (MultiPollerService));
		private readonly string _name;

		public Service(string name)
		{
			_name = name;
		}

		public TimeSpan RunIteration()
		{
			Log.InfoFormat("{0}: iteration", _name);
			if (_name == "svc3")
			{
				throw new ApplicationException("Oops!");
			}
			Thread.Sleep(TimeSpan.FromSeconds(1));
			return TimeSpan.Zero;
		}

		public IService Create()
		{
			Log.InfoFormat("{0}: creation", _name);
			return this;
		}
	}
}
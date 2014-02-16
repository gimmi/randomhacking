using System;
using System.Threading;
using Autofac;
using Topshelf;
using log4net;
using log4net.Config;

namespace MultiPollerService
{
	public class Program
	{
		public static void Main()
		{
			BasicConfigurator.Configure();

			var builder = new ContainerBuilder();
			builder.Register(c => new Poller("svc1")).Named<IPoller>("svc1").InstancePerLifetimeScope();
			builder.Register(c => new Poller("svc2")).Named<IPoller>("svc2").InstancePerLifetimeScope();
			builder.Register(c => new Poller("svc3")).Named<IPoller>("svc3").InstancePerLifetimeScope();
			IContainer container = builder.Build();

			var multiService = new MultiPoller(container, new[] { "svc1", "svc2", "svc4" }, TimeSpan.FromSeconds(5));

			HostFactory.Run(hcfg => {
				hcfg.UseLog4Net();
				hcfg.Service<MultiPoller>(scfg => {
					scfg.ConstructUsing(() => multiService);
					scfg.WhenStarted(svc => svc.Start());
					scfg.WhenStopped(svc => svc.Stop());
				});
			});
		}
	}

	public class Poller : IPoller
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof (MultiPoller));
		private readonly string _name;

		public Poller(string name)
		{
			_name = name;
		}

		public TimeSpan Poll()
		{
			Log.InfoFormat("{0}: iteration", _name);
			if (_name == "svc3")
			{
				throw new ApplicationException("Oops!");
			}
			Thread.Sleep(TimeSpan.FromSeconds(1));
			return TimeSpan.Zero;
		}

		public IPoller Create()
		{
			Log.InfoFormat("{0}: creation", _name);
			return this;
		}
	}
}
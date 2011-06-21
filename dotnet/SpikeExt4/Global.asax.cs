using System;
using System.Web;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ExtDirectHandler;
using ExtDirectHandler.Configuration;
using NHibernate;
using NHibernate.Cfg;
using log4net;
using log4net.Config;

namespace SpikeExt4
{
	public class Global : HttpApplication
	{
		private static ILog _logger;
		private static IWindsorContainer _container;

		protected void Application_Start(object sender, EventArgs e)
		{
			XmlConfigurator.Configure();
			_logger = LogManager.GetLogger(typeof(Global));
			_logger.Info("Starting application");

			_container = new WindsorContainer();
			_container.Register(
				Component.For<TicketRepository>().LifeStyle.Transient,
				Component.For<FilterClauseRepository>().LifeStyle.Transient,
				Component.For<ISessionFactory>().UsingFactoryMethod(CreateSessionFactory),
				Component.For<ISession>().UsingFactoryMethod(CreateSession).LifeStyle.PerWebRequest
				);

			DirectHttpHandler.SetMetadata(new ReflectionConfigurator()
			                              	.RegisterType<TicketRepository>()
			                              	.RegisterType<FilterClauseRepository>()
			                              	.BuildMetadata());
			DirectHttpHandler.SetObjectFactory(new WindsorObjectFactory());
		}

		private static ISessionFactory CreateSessionFactory()
		{
			return new Configuration()
				.Configure()
				.AddAssembly(typeof(Global).Assembly)
				.BuildSessionFactory();
		}

		private static ISession CreateSession(IKernel kernel)
		{
			var sessionFactory = kernel.Resolve<ISessionFactory>();
			try
			{
				return sessionFactory.OpenSession();
			}
			finally
			{
				kernel.ReleaseComponent(sessionFactory);
			}
		}

		protected void Session_Start(object sender, EventArgs e) {}

		protected void Application_BeginRequest(object sender, EventArgs e) {}

		protected void Application_AuthenticateRequest(object sender, EventArgs e) {}

		protected void Application_Error(object sender, EventArgs e) {}

		protected void Session_End(object sender, EventArgs e) {}

		protected void Application_End(object sender, EventArgs e) {}

		#region Nested type: WindsorObjectFactory

		public class WindsorObjectFactory : ObjectFactory
		{
			public override object GetInstance(Type type)
			{
				return _container.Resolve(type);
			}

			public override void Release(object instance)
			{
				_container.Release(instance);
			}
		}

		#endregion
	}
}
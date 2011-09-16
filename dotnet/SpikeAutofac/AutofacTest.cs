using System;
using Autofac;
using NUnit.Framework;

namespace SpikeAutofac
{
	[TestFixture]
	public class AutofacTest
	{
		public const string UnitOfWorkScope = "uow";

		[Test]
		public void tt()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<Repository>().InstancePerLifetimeScope();
			builder.RegisterType<Session>().InstancePerMatchingLifetimeScope(UnitOfWorkScope);
			var container = builder.Build();

			using (var uow = container.BeginLifetimeScope(UnitOfWorkScope))
			{
				var repository = uow.Resolve<Repository>();
				var session = repository._session.Invoke();
			}
		}

		public class Session
		{
			
		}

		public class Repository
		{
			public readonly Func<Session> _session;

			public Repository(Func<Session> session)
			{
				_session = session;
			}
		}
	}
}
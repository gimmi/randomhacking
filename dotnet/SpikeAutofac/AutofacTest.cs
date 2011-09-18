using System;
using Autofac;
using NUnit.Framework;
using SharpTestsEx;

namespace SpikeAutofac
{
	[TestFixture]
	public class AutofacTest
	{
		public const string UnitOfWorkScope = "uow";

		[Test]
		public void Should_resolve_lazy_components()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<A>();
			var container = builder.Build();

			var a = container.Resolve<A>();
			a = container.Resolve<Lazy<A>>().Value;
			a = container.Resolve<Func<A>>().Invoke();
		}

		[Test]
		public void Should_not_keep_track_of_transient_components()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<A>().InstancePerDependency();
			A a;
			using (var container = builder.Build())
			{
				a = container.Resolve<A>();
			}
			a.Disposed.Should().Be.False();
		}

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

		public class A : IDisposable
		{
			public bool Disposed;

			public void Dispose()
			{
				Disposed = true;
			}
		}
	}
}
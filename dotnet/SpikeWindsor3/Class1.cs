using System;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.Windsor;
using NUnit.Framework;

namespace SpikeWindsor3
{
	[TestFixture]
	public class Class1
	{
		[Test]
		public void Shoul_fail_to_resolve_scoped_components_when_out_of_the_scope()
		{
			var container = new WindsorContainer();
			container.Register(Component.For<AImpl>().LifestyleScoped());

			var e = Assert.Throws<InvalidOperationException>(() => container.Resolve<AImpl>());
			StringAssert.Contains("Scope was not available", e.Message);
		}

		[Test]
		public void Should_release_scoped_component_when_scope_is_disposed()
		{
			var container = new WindsorContainer();
			container.Register(Component.For<AImpl>().LifestyleScoped());

			AImpl aImpl;
			using(container.BeginScope())
			{
				aImpl = container.Resolve<AImpl>();
			}

			Assert.IsTrue(aImpl.Disposed);
		}

		[Test]
		public void Should_release_scoped_component_when_scope_is_disposed2()
		{
			var container = new WindsorContainer();
			container.Register(Component.For<AImpl>().LifestyleScoped());

			AImpl aImpl;
			using(container.BeginScope())
			{
				aImpl = container.Resolve<AImpl>();
			}

			Assert.IsTrue(aImpl.Disposed);
		}

		[Test]
		public void Should_resolve_lazy_components()
		{
			var container = new WindsorContainer();
			container.Register(Component.For<LazyOfTComponentLoader>());
			container.Register(Component.For<IA>().ImplementedBy<AImpl>().LifeStyle.Transient);

			var a = container.Resolve<IA>();
			var funcA = container.Resolve<Func<IA>>();
			var lazyA = container.Resolve<Lazy<IA>>();
		}

		[Test]
		public void Should_release_lazy_scoped_component_when_scope_is_disposed()
		{
			var container = new WindsorContainer();
			container.Register(Component.For<LazyOfTComponentLoader>());
			container.Register(Component.For<AImpl>().LifestyleScoped());

			Lazy<AImpl> a;
			using(container.BeginScope())
			{
				a = container.Resolve<Lazy<AImpl>>();
			}

			Assert.IsTrue(a.Value.Disposed);
		}

		public interface IA : IDisposable
		{
			 
		}

		public class AImpl : IA
		{
			public bool Disposed;

			public void Dispose()
			{
				Disposed = true;
			}
		}
	}
}
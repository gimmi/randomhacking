using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.Windsor;
using NUnit.Framework;

namespace SpikeWindsor3
{
	[TestFixture]
	public class LazyComponentTest
	{
		private WindsorContainer _target;

		[SetUp]
		public void SetUp()
		{
			_target = new WindsorContainer();
		}

		[TearDown]
		public void TearDown()
		{
			_target.Dispose();
			_target = null;
		}

		[Test]
		public void Should_resolve_lazy_components()
		{
			_target.Register(Component.For<ILazyComponentLoader>().ImplementedBy<LazyOfTComponentLoader>());
			_target.Register(Component.For<ISomeService>().ImplementedBy<SomeService>().LifeStyle.Singleton);

			var a = _target.Resolve<ISomeService>();
			var lazyA = _target.Resolve<Lazy<ISomeService>>();

			Assert.AreSame(a, lazyA.Value);
		}
	}
}
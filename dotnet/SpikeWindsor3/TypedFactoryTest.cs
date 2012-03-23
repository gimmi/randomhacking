using System;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace SpikeWindsor3
{
	[TestFixture]
	public class TypedFactoryTest
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
		public void Should_resolve_func_components()
		{
			_target.AddFacility<TypedFactoryFacility>();
			_target.Register(Component.For<ISomeService>().ImplementedBy<SomeService>().LifeStyle.Singleton);

			var a = _target.Resolve<ISomeService>();
			var funcA = _target.Resolve<Func<ISomeService>>();

			Assert.AreSame(a, funcA.Invoke());
		}
	}
}
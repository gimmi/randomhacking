using System;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace SpikeWindsor3
{
	[TestFixture]
	public class CustomScopeTest
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
		public void Shoul_fail_to_resolve_scoped_components_when_out_of_the_scope()
		{
			_target.Register(Component.For<DisposableService>().LifestyleScoped<CustomScope>());

			var e = Assert.Throws<InvalidOperationException>(() => _target.Resolve<DisposableService>());
			StringAssert.Contains("Scope was not available", e.Message);
		}

		[Test]
		public void Shoul_resolve_when_in_scope_scope()
		{
			_target.Register(Component.For<DisposableService>().LifestyleScoped<CustomScope>());

			DisposableService service;
			CustomScope.BeginScope();
			service = _target.Resolve<DisposableService>();
			Assert.NotNull(service);
			Assert.AreSame(service, _target.Resolve<DisposableService>());
			CustomScope.EndScope();

			Assert.True(service.Disposed);
		}

		[Test]
		public void Shoul_fail_when_nesting_scope()
		{
			CustomScope.BeginScope();
			Assert.Throws<InvalidOperationException>(CustomScope.BeginScope);
			CustomScope.EndScope();
		}
	}
}
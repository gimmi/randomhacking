using System;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;
using NUnit.Framework;

namespace SpikeWindsor3
{
	[TestFixture]
	public class ScopedLifestyleTest
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
			_target.Register(Component.For<SomeService>().LifestyleScoped());

			var e = Assert.Throws<InvalidOperationException>(() => _target.Resolve<SomeService>());
			StringAssert.Contains("Scope was not available", e.Message);
		}

		[Test]
		public void Should_release_scoped_component_when_scope_is_disposed()
		{
			_target.Register(Component.For<SomeService>().LifestyleScoped());

			SomeService someService;
			using(_target.BeginScope())
			{
				someService = _target.Resolve<SomeService>();
			}

			Assert.IsTrue(someService.Disposed);
		}

		[Test]
		public void Should_allow_nested_scopes()
		{
			_target.Register(Component.For<SomeService>().LifestyleScoped());

			SomeService instance;
			using(_target.BeginScope())
			{
				instance = _target.Resolve<SomeService>();
				SomeService otherInstance;
				using(_target.BeginScope())
				{
					otherInstance = _target.Resolve<SomeService>();
					Assert.AreNotSame(instance, otherInstance);
				}
				Assert.IsTrue(otherInstance.Disposed);
				Assert.IsFalse(instance.Disposed);
			}
			Assert.IsTrue(instance.Disposed);
		}

		[Test]
		public void Should_count_tracked_components()
		{
			_target.Register(Component.For<SomeService>().LifestyleTransient());

			Assert.AreEqual(0, GetTrackedComponentcount());

			var someService = _target.Resolve<SomeService>();

			Assert.AreEqual(1, GetTrackedComponentcount());

			_target.Release(someService);

			Assert.IsTrue(someService.Disposed);
			Assert.AreEqual(0, GetTrackedComponentcount());
		}

		private int GetTrackedComponentcount()
		{
			var diagnosticsHost = (IDiagnosticsHost)_target.Kernel.GetSubSystem(SubSystemConstants.DiagnosticsKey);
			var trackedComponentsDiagnostic = diagnosticsHost.GetDiagnostic<ITrackedComponentsDiagnostic>();
			return trackedComponentsDiagnostic.Inspect().Count;
		}
	}
}
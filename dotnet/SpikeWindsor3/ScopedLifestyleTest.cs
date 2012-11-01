using System;
using System.Threading;
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
            _target.Register(Component.For<DisposableService>().LifestyleScoped());

            var e = Assert.Throws<InvalidOperationException>(() => _target.Resolve<DisposableService>());
            StringAssert.Contains("Scope was not available", e.Message);
        }

        [Test]
        public void Should_release_scoped_component_when_scope_is_disposed()
        {
            _target.Register(Component.For<DisposableService>().LifestyleScoped());

            DisposableService disposableService;
            using(_target.BeginScope())
            {
                disposableService = _target.Resolve<DisposableService>();
            }

            Assert.IsTrue(disposableService.Disposed);
        }

        [Test]
        public void Should_allow_nested_scopes()
        {
            _target.Register(Component.For<DisposableService>().LifestyleScoped());

            DisposableService instance;
            using(_target.BeginScope())
            {
                instance = _target.Resolve<DisposableService>();
                DisposableService nestedInstance;
                using(_target.BeginScope())
                {
                    nestedInstance = _target.Resolve<DisposableService>();
                    Assert.AreNotSame(instance, nestedInstance);
                }
                Assert.IsTrue(nestedInstance.Disposed);
                Assert.IsFalse(instance.Disposed);
            }
            Assert.IsTrue(instance.Disposed);
        }

        [Test]
        public void Should_count_tracked_components()
        {
            _target.Register(Component.For<DisposableService>().LifestyleTransient());

            Assert.AreEqual(0, GetTrackedComponentcount());

            var someService = _target.Resolve<DisposableService>();

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


        [Test]
        public void Scope_should_be_different_in_different_threads()
        {
            _target.Register(Component.For<DisposableService>().LifestyleScoped());

            DisposableService svc1 = null;
            DisposableService svc2 = null;
            var thread1 = new Thread(() => {
                using(_target.BeginScope())
                {
                    svc1 = _target.Resolve<DisposableService>();
                }
            });
            var thread2 = new Thread(() => {
                using(_target.BeginScope())
                {
                    svc2 = _target.Resolve<DisposableService>();
                }
            });
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            Assert.True(svc1.Disposed);
            Assert.True(svc2.Disposed);
            Assert.AreNotSame(svc1, svc2);
        }
    }
}
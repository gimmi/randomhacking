using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Autofac;
using Autofac.Core;
using NUnit.Framework;
using SharpTestsEx;

namespace MultiPollerService
{
	[TestFixture]
	public class MultiPollerTest
	{
		public const int TestSlowness = 100;
		public static ConcurrentQueue<string> Log;

		[SetUp]
		public void SetUp()
		{
			Log = new ConcurrentQueue<string>();
		}

		private MultiPoller _target;

		[Test]
		public void Should_poll()
		{
			var builder = new ContainerBuilder();
			builder.Register(c => new TestPoller("p1", 0, 10)).Named<IPoller>("svc1").SingleInstance();

			var sut = new MultiPoller(builder.Build(), new []{"svc1"}, TimeSpan.Zero);

			sut.Start();
			WaitFor(45);
			sut.Stop();

			Log.ToArray().Should().Have.SameSequenceAs(new[] {
				"p1 ctor",
				"p1 poll",
				"p1 poll", 
				"p1 poll", 
				"p1 poll", 
				"p1 poll"
			});
		}

		[Test]
		public void Should_delegate_to_ioc_poller_lifetime()
		{
			var builder = new ContainerBuilder();
			builder.Register(c => new TestPoller("p1", 0, 10)).Named<IPoller>("svc1").InstancePerLifetimeScope();

			var sut = new MultiPoller(builder.Build(), new []{"svc1"}, TimeSpan.Zero);

			sut.Start();
			WaitFor(45);
			sut.Stop();

			Log.ToArray().Should().Have.SameSequenceAs(new[] {
				"p1 ctor",
				"p1 poll",
				"p1 ctor",
				"p1 poll", 
				"p1 ctor",
				"p1 poll", 
				"p1 ctor",
				"p1 poll", 
				"p1 ctor",
				"p1 poll"
			});
		}

		[Test]
		public void Should_execute_pollers_in_parallel()
		{

			var builder = new ContainerBuilder();
			builder.Register(c => new TestPoller("p1", 0, 10)).Named<IPoller>("svc1").SingleInstance();
			builder.Register(c => new TestPoller("p2", 2, 10)).Named<IPoller>("svc2").SingleInstance();

			var container = builder.Build();

			var sut = new MultiPoller(container, new[] { "svc1", "svc2" }, TimeSpan.Zero);

			sut.Start();
			WaitFor(48);
			sut.Stop();

			Log.ToArray().Should().Have.SameSequenceAs(new[] {
				"p1 ctor",
				"p2 ctor",
				"p1 poll",
				"p2 poll",
				"p1 poll",
				"p2 poll",
				"p1 poll",
				"p2 poll",
				"p1 poll",
				"p2 poll",
				"p1 poll",
				"p2 poll",
			});
		}

		[Test]
		public void Failing_poller_should_not_interfere_with_other_pollers()
		{
			var builder = new ContainerBuilder();
			builder.Register(c => new TestPoller("p1", 0, 10)).Named<IPoller>("svc1").SingleInstance();
			builder.Register(c => new FailingPoller("p2", 2, 10)).Named<IPoller>("svc2").SingleInstance();

			var sut = new MultiPoller(builder.Build(), new[] { "svc1", "svc2" }, TimeSpan.Zero);

			sut.Start();
			WaitFor(48);
			sut.Stop();

			Log.ToArray().Should().Have.SameSequenceAs(new[] {
				"p1 ctor",
				"p2 ctor",
				"p1 poll",
				"p2 error",
				"p1 poll",
				"p2 error",
				"p1 poll",
				"p2 error",
				"p1 poll",
				"p2 error",
				"p1 poll",
				"p2 error",
			});
		}

		public static void WaitFor(int ms)
		{
			Thread.Sleep(ms * TestSlowness);
		}

		public class TestPoller : IPoller
		{
			private readonly string _name;
			private readonly int _pollMs;

			public TestPoller(string name, int ctorMs, int pollMs)
			{
				_name = name;
				_pollMs = pollMs;
				WaitFor(ctorMs);
				Log.Enqueue(string.Concat(_name, " ctor"));
			}

			public TimeSpan Poll()
			{
				WaitFor(_pollMs);
				Log.Enqueue(string.Concat(_name, " poll"));
				return TimeSpan.Zero;
			}
		}

		public class FailingPoller : IPoller
		{
			private readonly string _name;
			private readonly int _pollMs;

			public FailingPoller(string name, int ctorMs, int pollMs)
			{
				_name = name;
				_pollMs = pollMs;
				WaitFor(ctorMs);
				Log.Enqueue(string.Concat(_name, " ctor"));
			}

			public TimeSpan Poll()
			{
				WaitFor(_pollMs);
				Log.Enqueue(string.Concat(_name, " error"));
				throw new Exception("AHHH!");
			}
		}
	}
}
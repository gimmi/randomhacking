/*using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using SharpTestsEx;

namespace MultiPollerService
{
	[TestFixture]
	public class MultiPollerServiceTest
	{
		public const int TestSlowness = 100;

		[SetUp]
		public void SetUp()
		{
			_log = new ConcurrentQueue<string>();
		}

		[TearDown]
		public void TearDown()
		{
			if (_target != null)
			{
				_target.Dispose();
				_target = null;
			}
		}

		private MultiPoller _target;
		private ConcurrentQueue<string> _log;

		[Test]
		public void Should_execute_service_in_loop()
		{
			_target = new MultiPoller(new Func<IPoller>[] {
				() => new TestPoller("S1", _log)
			}, TimeSpan.Zero);

			_target.Start();
			Thread.Sleep(45 * TestSlowness);
			_target.Stop();

			_log.ToArray().Should().Have.SameSequenceAs(new[] {
				"S1: work", 
				"S1: work", 
				"S1: work", 
				"S1: work", 
				"S1: work"
			});
		}

		[Test]
		public void Should_execute_services_in_parallel()
		{
			_target = new MultiPoller(new Func<IPoller>[] {
				() => new TestPoller("S1", _log),
				() => new TestPoller("S2", _log)
			}, TimeSpan.Zero);

			_target.Start();
			Thread.Sleep(45 * TestSlowness);
			_target.Stop();

			_log.ToArray().Should().Have.SameSequenceAs(new[] {
				"S1: create", 
				"S2: create", 
				"S1: work", 
				"S2: work", 
				"S1: work", 
				"S2: work", 
				"S1: work", 
				"S2: work", 
				"S1: work", 
				"S2: work", 
				"S1: work",
				"S2: work"
			});
		}
		// ["S1: create", "S2: create", "S1: work", "S2: work", "S1: work", "S2: work", "S1: work", "S2: work", "S1: work", "S2: work", "S1: work", "S2: work"] 
		// ["S1: create", "S2: create", "S1: work", "S2: work", "S1: work", "S2: work", "S1: work", "S2: work", "S1: work", "S2: work", "S1: work"]
		[Test]
		public void Should_reinit_after_exception()
		{
			_target = new MultiPoller(new Func<IPoller>[] {
				() => new TestPoller("S1", _log) { ExceptionFreq = 2 }
			}, TimeSpan.Zero);

			_target.Start();
			Thread.Sleep(45 * TestSlowness);
			_target.Stop();

			_log.ToArray().Should().Have.SameSequenceAs(new[] {
				"S1: create", 
				"S1: work", 
				"S1: exception", 
				"S1: create", 
				"S1: work", 
				"S1: exception",
				"S1: create", 
				"S1: work"
			});
		}

		[Test]
		public void Failing_service_should_not_interfere_with_other_services()
		{
			_target = new MultiPoller(new Func<IPoller>[] {
				() => new TestPoller("S1", _log),
				() => new TestPoller("S2", _log) { ExceptionFreq = 1 },
			}, TimeSpan.Zero);

			_target.Start();
			Thread.Sleep(45 * TestSlowness);
			_target.Stop();

			_log.Where(x=>x.StartsWith("S1")).Should().Have.SameSequenceAs(new[] {
				"S1: create", 
				"S1: work", 
				"S1: work", 
				"S1: work", 
				"S1: work", 
				"S1: work"
			});
		}

//		[Test]
//		public void Should_keep_running_when_one_service_fail()
//		{
//			var svc1 = new TestPoller("S1", _log);
//			var svc2 = new TestPoller("S2", _log, new[] {3, 4});
//			_target = new MultiPoller(new[] {svc1, svc2}, TimeSpan.Zero);
//
//			_target.Start();
//			Thread.Sleep(TimeSpan.FromMilliseconds(100));
//			_target.Stop();
//
//			_log.ToArray().Where(x => x.StartsWith("S1")).Should().Have.SameSequenceAs(new[] {
//				"S1: create",
//				"S1: work",
//				"S1: work",
//				"S1: work",
//				"S1: work",
//				"S1: work",
//				"S1: work",
//				"S1: work",
//				"S1: work",
//				"S1: work",
//				"S1: work"
//			});
//			// 0 10 20 30 40 50 60 70 80 90 100
//
//			_log.Where(x => x.StartsWith("S2")).Should().Have.SameSequenceAs(new[] {
//				"S2: create",
//				"S2: work",
//				"S2: work",
//				"S2: exception",
//				"S2: create",
//				"S2: exception",
//				"S2: create",
//				"S2: work",
//				"S2: work",
//				"S2: work"
//			});
//		}
//
//		[Test]
//		public void Should_run_services_in_parallel()
//		{
//			var svc1 = new TestPoller("svc1", _log);
//			var svc2 = new TestPoller("svc2", _log);
//			_target = new MultiPoller(new[] {svc1, svc2}, TimeSpan.Zero);
//
//			_target.Start();
//			Thread.Sleep(TimeSpan.FromMilliseconds(450));
//			_target.Stop();
//
//			_log.ToArray().Should().Have.SameSequenceAs(new[] {
//				"svc1: creation #1", "svc2: work #1", "svc1: work #1", "svc1: work #2", "svc1: work #3", "svc2: work #2", "svc1: work #4", "svc2: work #3", "svc1: work #5"
//			});
//		}
	}

	public class TestPoller : IPoller
	{
		public int ExceptionFreq = int.MaxValue;
		private readonly ConcurrentQueue<string> _log;
		private readonly string _name;
		private int _counter;
		public int WorkDelay = 10;

		public TestPoller(string name, ConcurrentQueue<string> log)
		{
			_log = log;
			_name = name;
		}

		public TimeSpan Poll()
		{
			Thread.Sleep(WorkDelay * MultiPollerServiceTest.TestSlowness);
			if ((++_counter % ExceptionFreq) == 0)
			{
				Log("exception");
				throw new ApplicationException();
			}
			Log("work");
			return TimeSpan.Zero;
		}

		private void Log(string action)
		{
			_log.Enqueue(string.Concat(_name, ": ", action));
		}
	}
}*/
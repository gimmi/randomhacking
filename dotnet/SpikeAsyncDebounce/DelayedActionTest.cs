using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SpikeAsyncDebounce
{
    public class DelayedActionTest
    {
/*
        public static void Main()
        {
            var action = new DebouncedAction(TimeSpan.FromSeconds(2), async () => {
                await Console.Out.WriteLineAsync("Invoked!");
            });

            Console.WriteLine("Press any key to invoke, X to exit");

            while (Console.ReadKey().Key != ConsoleKey.X)
            {
                action.Invoke();
            }


            Console.Out.WriteLine();
            Console.Out.WriteLine("Disposing");
            action.Dispose();
        }
*/

        [Test]
        public async Task Should_invoke_as_expected()
        {
            var count = 0;
            var sut = new DelayedAction(TimeSpan.FromSeconds(.5), () => Interlocked.Increment(ref count));
            Assert.That(sut.HasBeenInvoked, Is.False);

            await Task.Delay(1_000);
            Assert.That(count, Is.EqualTo(0));
            Assert.That(sut.HasBeenInvoked, Is.False);

            sut.Reset();

            await Task.Delay(200);
            Assert.That(count, Is.EqualTo(0));
            Assert.That(sut.HasBeenInvoked, Is.False);

            await Task.Delay(500);
            Assert.That(count, Is.EqualTo(1));
            Assert.That(sut.HasBeenInvoked, Is.True);

            await Task.Delay(1_000);
            Assert.That(count, Is.EqualTo(1));
            Assert.That(sut.HasBeenInvoked, Is.True);

            sut.Reset();

            await Task.Delay(250);
            Assert.That(count, Is.EqualTo(1));
            Assert.That(sut.HasBeenInvoked, Is.False);

            sut.Reset();

            await Task.Delay(250);
            Assert.That(count, Is.EqualTo(1));
            Assert.That(sut.HasBeenInvoked, Is.False);

            await Task.Delay(500);
            Assert.That(count, Is.EqualTo(2));
            Assert.That(sut.HasBeenInvoked, Is.True);

            await sut.DisposeAsync();
        }

        [Test]
        public async Task Should_dispose_async()
        {
            var count = 0;
            var sut = new DelayedAction(TimeSpan.FromSeconds(.5), () => Interlocked.Increment(ref count));

            sut.Reset();

            await sut.DisposeAsync();
        }

        [Test]
        public void Should_tolerate_concurrent_invoke_calls()
        {
            var callCount = 0;
            var sut = new DelayedAction(TimeSpan.FromSeconds(.5), () => Interlocked.Increment(ref callCount));

            var semaphore = new SemaphoreSlim(0);

            var threads = Enumerable.Range(0, 100)
                .Select(_ => new Thread(() => {
                    semaphore.Wait();
                    sut.Reset();
                }))
                .ToArray();
            foreach (var thread in threads)
            {
                thread.Start();
            }

            semaphore.Release(100);

            Thread.Sleep(1_000);

            Assert.That(callCount, Is.EqualTo(1));

            foreach (var thread in threads)
            {
                thread.Join();
            }

            sut.Dispose();
        }
    }
}

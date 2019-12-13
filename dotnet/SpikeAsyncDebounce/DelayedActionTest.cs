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

            await Task.Delay(1_000);
            Assert.That(count, Is.EqualTo(0));

            sut.Reset();

            await Task.Delay(200);
            Assert.That(count, Is.EqualTo(0));

            await Task.Delay(500);
            Assert.That(count, Is.EqualTo(1));

            await Task.Delay(1_000);
            Assert.That(count, Is.EqualTo(1));

            sut.Reset();

            await Task.Delay(250);
            Assert.That(count, Is.EqualTo(1));

            sut.Reset();

            await Task.Delay(250);
            Assert.That(count, Is.EqualTo(1));

            await Task.Delay(500);
            Assert.That(count, Is.EqualTo(2));

            sut.Dispose();
        }

        [Test]
        public async Task Should_tolerate_concurrent_invoke_calls()
        {
            var count = 0;
            var sut = new DelayedAction(TimeSpan.FromSeconds(.5), () => Interlocked.Increment(ref count));

            await Task.WhenAll(Enumerable.Range(0, 100).Select(_ => Task.Run(() => {
                for (var i = 0; i < 1_000; i++)
                {
                    sut.Reset();
                }
            })));

            Assert.That(count, Is.EqualTo(0));

            await Task.Delay(1_000);

            Assert.That(count, Is.EqualTo(1));

            sut.Dispose();
        }
    }
}

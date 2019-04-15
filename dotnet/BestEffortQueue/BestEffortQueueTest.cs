using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BestEffortQueue
{
    public class BestEffortQueueTest
    {
        [Test]
        public async Task Should_enqueue_and_dequeue()
        {
            var sut = new BestEffortQueue<int>(2);

            Assert.That(sut.TryEnqueue(1), Is.True);
            Assert.That(sut.LossCount, Is.EqualTo(0));
            Assert.That(sut.TryEnqueue(2), Is.True);
            Assert.That(sut.LossCount, Is.EqualTo(0));
            Assert.That(sut.TryEnqueue(3), Is.False);
            Assert.That(sut.LossCount, Is.EqualTo(1));

            var ct = new CancellationTokenSource(1_000).Token;
            Assert.That(sut.Current, Is.Zero);
            Assert.That(await sut.MoveNextAsync(ct), Is.True);
            Assert.That(sut.Current, Is.EqualTo(1));
            Assert.That(await sut.MoveNextAsync(ct), Is.True);
            Assert.That(sut.Current, Is.EqualTo(2));
            Assert.That(await sut.MoveNextAsync(ct), Is.False);
        }
        
        [Test]
        public async Task Should_allow_concurrent_enqueue()
        {
            var sut = new BestEffortQueue<int>(50);
            
            var enqueueResults = new ConcurrentBag<bool>();
            var dequeueResults = new ConcurrentBag<int>();

            await Task.WhenAll(
                Enumerable.Range(1, 100)
                    .Select(Producer)
                    .Prepend(Consumer())
            );

            var succeededEnqueues = enqueueResults.Count(success => success);
            var failedEnqueues = enqueueResults.Count(success => !success);
            Assert.That(succeededEnqueues, Is.InRange(40, 60));
            Assert.That(failedEnqueues, Is.InRange(40, 60));
            Assert.That(sut.LossCount, Is.EqualTo(failedEnqueues));
            Assert.That(dequeueResults.Count, Is.EqualTo(succeededEnqueues));
            Assert.That(dequeueResults.Distinct().Count(), Is.EqualTo(succeededEnqueues));

            async Task Producer(int i)
            {
                await Task.Yield();
                enqueueResults.Add(sut.TryEnqueue(i));
            }

            async Task Consumer()
            {
                await Task.Yield();
                var ct = new CancellationTokenSource(1_000).Token;
                while (await sut.MoveNextAsync(ct))
                {
                    dequeueResults.Add(sut.Current);
                }
            }
        }
        
        [Test]
        public async Task Should_stop_consuming_after_cancellation()
        {
            var sut = new BestEffortQueue<string>(100);
            sut.TryEnqueue("1");

            Assert.That(await sut.MoveNextAsync(CancellationToken.None), Is.True);
            Assert.That(sut.Current, Is.EqualTo("1"));

            var moveNextTask = Task.Run(() => sut.MoveNextAsync(new CancellationTokenSource(10).Token));
            await Task.Delay(100);
            sut.TryEnqueue("2");
            Assert.That(await moveNextTask, Is.False);
            Assert.That(sut.Current, Is.Null);

            var cts = new CancellationTokenSource();
            cts.Cancel();
            Assert.That(await sut.MoveNextAsync(cts.Token), Is.False);
            Assert.That(sut.Current, Is.Null);
            
            Assert.That(await sut.MoveNextAsync(CancellationToken.None), Is.True);
            Assert.That(sut.Current, Is.EqualTo("2"));
        }
    }
}
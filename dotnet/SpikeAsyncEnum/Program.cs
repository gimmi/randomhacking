using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpikeAsyncEnum
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var enumerable = new MyAsyncEnumerable();
            await Task.WhenAll(
                Task.Run(async () => {
                    var i = 0;
                    while (true)
                    {
                        await Task.Delay(1_000);
                        await enumerable.PublishAsync("Elem " + ++i);
                    }
                }),
                Task.Run(async () => {
                    await foreach (var elem in enumerable)
                    {
                        await Console.Out.WriteLineAsync(elem);
                    }
                })
            );
        }
    }

    public class MyAsyncEnumerable : IAsyncEnumerable<string>
    {
        private readonly ConcurrentDictionary<Guid, TaskCompletionSource<string>> _enumerators = new ConcurrentDictionary<Guid, TaskCompletionSource<string>>();

        public async Task PublishAsync(string value)
        {
            var tasks = _enumerators.Values.Select(tcs => {
                tcs.SetResult(value);
                return tcs.Task;
            });

            await Task.WhenAll(tasks);
        }

        public IAsyncEnumerator<string> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new MyAsyncEnumerator(_enumerators);
    }

    public class MyAsyncEnumerator : IAsyncEnumerator<string>
    {
        private readonly ConcurrentDictionary<Guid, TaskCompletionSource<string>> _enumerators;
        private readonly Guid _id;
        private string _current;

        public MyAsyncEnumerator(ConcurrentDictionary<Guid, TaskCompletionSource<string>> enumerators)
        {
            _enumerators = enumerators;
            _id = Guid.NewGuid(); // TODO use int

            _enumerators.TryAdd(_id, new TaskCompletionSource<string>());
        }

        public ValueTask DisposeAsync()
        {
            _enumerators.TryRemove(_id, out _);
            return new ValueTask();
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            var tcs = _enumerators.GetOrAdd(_id, _ => new TaskCompletionSource<string>());
            try
            {
                _current = await tcs.Task;
                return true;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
        }

        public string Current => _current;
    }
}

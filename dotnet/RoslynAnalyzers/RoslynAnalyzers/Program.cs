using System;
using System.Threading.Tasks;

namespace RoslynAnalyzers
{
    public static class Program
    {
        private static ValueTask _valueTask;

        public static async Task Main()
        {
            _valueTask = new MyAsyncDisposable().DisposeAsync();

            await Console.Out.WriteLineAsync("Hello World!");
        }
    }

    public class MyAsyncDisposable : IAsyncDisposable
    {
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}

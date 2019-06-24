using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncEvents
{
    public class Program
    {
        public static async Task Main()
        {
            await Console.Out.WriteLineAsync("He,l");
            
            var evt = new AsyncMulticastDelegate();
            
            await evt.InvokeAsync("x1", "x2");

            evt.Add(Handler1);

            await evt.InvokeAsync("y1", "y2");

            evt.Add(Handler2);

            await evt.InvokeAsync("z1", "z2");
            
            evt.Remove(Handler1);

            await evt.InvokeAsync("w1", "w2");
        }

        private static async Task Handler1(string arg1, string arg2)
        {
            await Console.Out.WriteLineAsync($"Handler1({arg1}, {arg2})");
        }

        private static async Task Handler2(string arg1, string arg2)
        {
            await Console.Out.WriteLineAsync($"Handler2({arg1}, {arg2})");
        }
    }

    public class AsyncMulticastDelegate
    {
        private IImmutableSet<Func<string, string, Task>> _handlers = ImmutableHashSet<Func<string, string, Task>>.Empty;

        public void Add(Func<string, string, Task> handler)
        {
            _handlers = _handlers.Add(handler);
        }

        public void Remove(Func<string, string, Task> handler)
        {
            _handlers = _handlers.Remove(handler);
        }

        public Task InvokeAsync(string s1, string s2)
        {
            return Task.WhenAll(_handlers.Select(h => h.Invoke(s1, s2)));
        }
    }
}

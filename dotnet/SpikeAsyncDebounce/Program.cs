using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpikeAsyncDebounce
{
    public class Program
    {
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
    }
}

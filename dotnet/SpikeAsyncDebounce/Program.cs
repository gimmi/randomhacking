using System;

namespace SpikeAsyncDebounce
{
    public class Program
    {
        public static void Main()
        {
            using var action = new DebouncedAction(TimeSpan.FromSeconds(2), async () => {
                await Console.Out.WriteLineAsync("Invoked!");
            });

            Console.WriteLine("Press any key to invoke...");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                action.Invoke();
            }
        }
    }
}

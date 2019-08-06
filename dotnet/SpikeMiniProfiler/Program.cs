using System;
using StackExchange.Profiling;

namespace SpikeMiniProfiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var profiler = MiniProfiler.StartNew("My Pofiler Name");
            using (profiler.Step("Main Work"))
            {
                Console.WriteLine("Hello World!");
            }

            Console.WriteLine(profiler.RenderPlainText());
        }
    }
}

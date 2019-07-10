using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace SpikeBenchmarkDotNet
{
//    [MemoryDiagnoser, ShortRunJob]
    [ShortRunJob]
    public class StructVsClass
    {
        private const int Count = 10_000;
        private ImmutableSortedDictionary<string, object> _isd;
        private ConcurrentDictionary<string, object> _d;
        
        [GlobalSetup]
        public void GlobalSetup()
        {
            _d = new ConcurrentDictionary<string, object>();
            var builder = ImmutableSortedDictionary.CreateBuilder<string, object>();
            foreach (var key in Enumerable.Range(0, Count).Select(i => "symbol-" + i)) 
            {
                builder.Add(key, new object());
                _d.TryAdd(key, new object());
            }
            _isd = builder.ToImmutable();
        }
        
        [Params("symbol-2500", "symbol-5000", "symbol-7500")]
        public string Key { get; set; }

        [Benchmark]
        public object ReadImmutable()
        {
            _isd.TryGetValue(Key, out var val);
            return val;
        }

        [Benchmark]
        public object ReadDictionary()
        {
            _d.TryGetValue(Key, out var val);
            return val;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<StructVsClass>();
        }
    }
}

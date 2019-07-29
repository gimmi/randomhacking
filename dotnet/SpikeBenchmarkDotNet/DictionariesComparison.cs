using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace SpikeBenchmarkDotNet
{
//    [MemoryDiagnoser, ShortRunJob]
    [ShortRunJob]
    public class DictionariesComparison
    {
        private ImmutableSortedDictionary<string, object> _isd;
        private ImmutableDictionary<string, object> _id;
        private ConcurrentDictionary<string, object> _concurrentDictionary;
        private Dictionary<string, object> _dictionary;

        [Params(100, 1_000)]
        public int Count { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _concurrentDictionary = new ConcurrentDictionary<string, object>();
            _dictionary = new Dictionary<string, object>();
            var idb = ImmutableDictionary.CreateBuilder<string, object>();
            foreach (var key in Enumerable.Range(0, Count).Select(i => "symbol-" + i))
            {
                idb.Add(key, new object());
                _concurrentDictionary.TryAdd(key, new object());
                _dictionary.TryAdd(key, new object());
            }
            _isd = idb.ToImmutableSortedDictionary();
            _id = idb.ToImmutableDictionary();
        }

        [Benchmark]
        public object ReadImmutableSorted()
        {
            _isd.TryGetValue("symbol-5000", out var val);
            return val;
        }

        [Benchmark]
        public object ReadImmutable()
        {
            _id.TryGetValue("symbol-5000", out var val);
            return val;
        }

        [Benchmark]
        public object ReadConcurrentDictionary()
        {
            _concurrentDictionary.TryGetValue("symbol-5000", out var val);
            return val;
        }

        [Benchmark]
        public object ReadDictionary()
        {
            _dictionary.TryGetValue("symbol-5000", out var val);
            return val;
        }
    }
}

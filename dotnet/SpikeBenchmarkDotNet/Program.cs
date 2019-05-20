using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace SpikeBenchmarkDotNet
{
    public class Md5VsSha256
    {
        private readonly string[] _strings;

        private readonly SHA256 sha256 = SHA256.Create();
        private readonly MD5 md5 = MD5.Create();

        public Md5VsSha256()
        {
            _strings = new[] {"String 1", "String 2", "String 3", "String 4", "String 5"};
        }

        [Benchmark]
        public string Concat() => string.Concat(_strings); 

        [Benchmark]
        public string Join() => string.Join("", _strings);
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Md5VsSha256>();
        }
    }
}

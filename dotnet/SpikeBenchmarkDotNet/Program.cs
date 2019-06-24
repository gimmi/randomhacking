using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace SpikeBenchmarkDotNet
{
    [MemoryDiagnoser, ShortRunJob]
    public class StructVsClass
    {
        private readonly string[] _strings;

        private readonly SHA256 sha256 = SHA256.Create();
        private readonly MD5 md5 = MD5.Create();

        public StructVsClass()
        {
            _strings = new[] {"String 1", "String 2", "String 3", "String 4", "String 5"};
        }
        
        [GlobalSetup]
        public void GlobalSetup()
        {
            bytes = new byte[100];
        }

        [Benchmark]
        public StrVal Str() => new StrVal(123, 456); 

        [Benchmark]
        public ClassVal Cls() => new ClassVal(123, 456);

        [Benchmark]
        public StructWithArray StructWithArray()
        { 
            return new StructWithArray(123, );
        }
    }

    public struct AmsAddress
    {
        private readonly ulong _val;

        public static AmsAddress Parse(string amsAddress)
        {
            var match = Regex.Match(amsAddress, @"^(\d+)\.(\d+)\.(\d+)\.(\d+)\.(\d+)\.(\d+)(:(?<port>\d+))?$");
            if (!match.Success)
            {
                throw new ApplicationException("Invalid AMS address: " + amsAddress);
            }
            
            ulong val = (ulong) (Convert.ToByte(match.Groups[1].Value) << 56) +
                 (ulong) (Convert.ToByte(match.Groups[2].Value) << 48) +
                 (ulong) (Convert.ToByte(match.Groups[3].Value) << 40) +
                 (ulong) (Convert.ToByte(match.Groups[4].Value) << 32) +
                 (ulong) (Convert.ToByte(match.Groups[5].Value) << 24) +
                 (ulong) (Convert.ToByte(match.Groups[6].Value) << 16);

            if (match.Groups["port"].Success)
            {
                val += Convert.ToUInt32(match.Groups["port"].Value);
            }
            return new AmsAddress(val);
        }

        public AmsAddress(ulong val)
        {
            _val = val;
        }

        public UInt16 Port { get; private set; }
        public byte[] ToBytes()
        {
            return new[] {
                
            }
        }
    }
    public struct StructWithArray
    {
        public readonly int Port;
        public readonly byte[] Bytes;

        public StructWithArray(int port, byte[] bytes)
        {
            Port = port;
            Bytes = bytes;
        }
    }

    public struct StrVal
    {
        public StrVal(int value1, int value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public readonly int Value1;
        public readonly int Value2;
    }

    public class ClassVal
    {
        public ClassVal(int value1, int value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public int Value1 { get; }
        public int Value2 { get; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<StructVsClass>();
        }
    }
}

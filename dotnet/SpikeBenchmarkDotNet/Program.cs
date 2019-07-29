using BenchmarkDotNet.Running;

namespace SpikeBenchmarkDotNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
//            BenchmarkRunner.Run<DictionariesComparison>();
            BenchmarkRunner.Run<NumericCasts>();
        }
    }
}

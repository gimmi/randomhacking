using BenchmarkDotNet.Attributes;

namespace SpikeBenchmarkDotNet
{
//    [ShortRunJob]
    public class NumericCasts
    {
        [Benchmark]
        public object FloatValueWithDoubleConstants()
        {
            float floatValue = 5;
            double offset = 3.14;
            double gain = 1.5;
            float result = (float) (floatValue * gain + offset);

            return result;
        }

        [Benchmark]
        public object FloatValueWithFloatConstants()
        {
            float floatValue = 5;
            float offset = 3;
            float gain = 2;
            float result = floatValue * gain + offset;

            return result;
        }

    }
}

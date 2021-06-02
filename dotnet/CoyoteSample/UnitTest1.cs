using Microsoft.Coyote;
using Microsoft.Coyote.SystematicTesting;
using Microsoft.Coyote.Tasks;
using NUnit.Framework;

namespace CoyoteSample
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [NUnit.Framework.Test]
        public void Test1()
        {
            var config = Configuration.Create().WithTestingIterations(1000);
            var engine = TestingEngine.Create(config, CoyoteTestMethod);
            engine.Run();

            Assert.That(engine.TestReport.NumOfFoundBugs, Is.Zero);
        }
        
        private async Task CoyoteTestMethod()
        {
            var counter = 0;

            await Task.WhenAll(
                Task.Run(() => counter++),
                Task.Run(() => counter++),
                Task.Run(() => counter++)
            );
            
            Assert.That(counter, Is.EqualTo(3));
        }
    }
}
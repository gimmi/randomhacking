using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;

namespace SpikeAsync
{
    public class AsyncDeadlockTest
    {
        [Test]
        public void Will_run_to_end()
        {
            Assert.That(SynchronizationContext.Current, Is.Null);

            var result = MethodAsync().Result;
            
            Assert.That(result, Is.EqualTo("result"));
        }

        [Test(Description = "See https://github.com/nunit/nunit/issues/2123")]
        public void Will_deadlock()
        {
            Assert.That(SynchronizationContext.Current, Is.Null);

            new Control().Dispose();
            
            Assert.That(SynchronizationContext.Current, Is.InstanceOf<WindowsFormsSynchronizationContext>());

            var result = MethodAsync().Result;
            
            Assert.That(result, Is.EqualTo("result"));
        }

        private async Task<string> MethodAsync()
        {
            await Task.Delay(100);
            return "result";
        }
    }
}
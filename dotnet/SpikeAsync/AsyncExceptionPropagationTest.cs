using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SpikeAsync
{
    [TestFixture]
    public class AsyncExceptionPropagationTest
    {
        [Test]
        public void Should_aggregate_multiple_exceptions()
        {
            try
            {
                Task.WhenAll(
                    Task.Run(() => throw new ApplicationException("1")),
                    Task.Run(() => throw new ApplicationException("2"))
                ).Wait();

                Assert.Fail();
            }
            catch (AggregateException e)
            {
                Assert.That(e.InnerExceptions, Has.Count.EqualTo(2));
            }
        }

        [Test]
        public void Should_propagate_exception_as_is()
        {
            try
            {
                Task.Run(() => throw new ApplicationException("1")).GetAwaiter().GetResult();

                Assert.Fail();
            }
            catch (ApplicationException e)
            {
                Assert.That(e.Message, Is.EqualTo("1"));
            }
        }

        [Test]
        public void Should_wrap_single_exception()
        {
            try
            {
                Task.Run(() => throw new ApplicationException("1")).Wait();

                Assert.Fail();
            }
            catch (AggregateException e)
            {
                Assert.That(e.InnerExceptions, Has.Count.EqualTo(1));
            }
        }

        [Test]
        public void Should_loose_multiple_exceptions()
        {
            try
            {
                Task.WhenAll(
                    Task.Run(() => throw new ApplicationException("1")),
                    Task.Run(() => throw new ApplicationException("2"))
                ).GetAwaiter().GetResult();

                Assert.Fail();
            }
            catch (ApplicationException e)
            {
                Assert.That(e.Message, Is.EqualTo("1"));
            }
        }

        [Test]
        public async Task Should_swallow_all_exceptions_but_one()
        {
            // See https://stackoverflow.com/questions/18314961

            try
            {
                await Task.WhenAll(
                    Task.Run(() => throw new ApplicationException("1")),
                    Task.Run(() => throw new ApplicationException("2"))
                );

                Assert.Fail();
            }
            catch (ApplicationException e)
            {
                Assert.That(e.Message, Is.EqualTo("1"));
            }
        }
    }
}
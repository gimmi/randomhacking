using System.Threading.Tasks;
using NUnit.Framework;

namespace SpikeBgWorker
{
	[TestFixture]
	public class ConsumerThreadTest
	{
		[SetUp]
		public void SetUp()
		{
			var sut = new ConsumerThread<int>((item, disposing) => {
				return true;
			});

			Parallel.For(0, 100, sut.Enqueue);
		}
	}
}
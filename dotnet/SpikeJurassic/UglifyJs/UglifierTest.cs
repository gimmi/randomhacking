using NUnit.Framework;

namespace SpikeJurassic.UglifyJs
{
	[TestFixture]
	public class UglifierTest
	{
		[Test]
		public void Tt()
		{
			Assert.That(new Uglifier().Uglify("(function () { var ciao = 1; })();"), Is.EqualTo("(function(){var a=1})()"));
		}
	}
}
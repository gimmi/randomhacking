using System.IO;
using System.Linq;
using NUnit.Framework;
using log4net;
using log4net.Appender;
using log4net.Config;

namespace SpikeWindsor3
{
	[SetUpFixture]
	public class SetUpFixture
	{
		static public MemoryAppender MemoryAppender;

		[SetUp]
		public void SetUp()
		{
			XmlConfigurator.Configure(new FileInfo("log4net.cfg.xml"));
			MemoryAppender = (MemoryAppender)LogManager.GetRepository().GetAppenders().Single(a => a.Name == "memory");
		}
	}
}
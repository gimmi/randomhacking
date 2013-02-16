using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using log4net;
using log4net.Appender;
using log4net.Config;

namespace NHibernateSetup
{
	[SetUpFixture]
	public class SetUpFixture
	{
		[SetUp]
		public void SetUp()
		{
			XmlConfigurator.Configure(new MemoryStream(Encoding.UTF8.GetBytes(@"
<log4net>
	<appender name='TraceAppender' type='log4net.Appender.TraceAppender'>
		<layout type='log4net.Layout.SimpleLayout' />
	</appender>	 
	<logger name='NHibernate.SQL' additivity='false'>
		<level value='ALL'/>
		<appender-ref ref='TraceAppender' />
	</logger>
</log4net>
")));
		}
	}
}
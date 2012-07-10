using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace SpikeLog4Net
{
	public class Program
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

		private static void Main()
		{
			log4net.Util.LogLog.InternalDebugging = true;
			DatabaseLog();
			XmlConfigurator.Configure();

			Log.Debug("debug");
			Log.Info("info");
			Log.Warn("warn");
			Log.Error("error");
			Log.Fatal("fatal");
		}

		private static void DatabaseLog()
		{
			XmlConfigurator.Configure(new MemoryStream(Encoding.UTF8.GetBytes(@"
<log4net>
	<appender name='AdoNetAppender' type='log4net.Appender.AdoNetAppender'>
		<bufferSize value='1' />
		<connectionType value='System.Data.SqlClient.SqlConnection' />
		<connectionString value='data source=.\SQLEXPRESS;initial catalog=ADOUtilstests;integrated security=true' />
		<commandText value='INSERT INTO [Logs](App, Thread, Level, Logger, Message, Exception) VALUES(@App, @Thread, @Level, @Logger, @Message, @Exception)' />
		<parameter>
			<parameterName value='@App' />
			<dbType value='String' />
			<size value='255' />
			<layout type='log4net.Layout.PatternLayout' value='%appdomain' />
		</parameter>
		<parameter>
			<parameterName value='@Thread' />
			<dbType value='String' />
			<size value='255' />
			<layout type='log4net.Layout.PatternLayout' value='%thread' />
		</parameter>
		<parameter>
			<parameterName value='@Level' />
			<dbType value='String' />
			<size value='50' />
			<layout type='log4net.Layout.PatternLayout' value='%level' />
		</parameter>
		<parameter>
			<parameterName value='@Logger' />
			<dbType value='String' />
			<size value='255' />
			<layout type='log4net.Layout.PatternLayout' value='%logger' />
		</parameter>
		<parameter>
			<parameterName value='@Message' />
			<dbType value='String' />
			<size value='-1' />
			<layout type='log4net.Layout.PatternLayout' value='%message' />
		</parameter>
		<parameter>
			<parameterName value='@Exception' />
			<dbType value='String' />
			<size value='-1' />
			<layout type='log4net.Layout.ExceptionLayout' />
		</parameter>
	</appender>
	<root>
		<level value='ALL' />
		<appender-ref ref='AdoNetAppender' />
	</root>
</log4net>
")));

			Log.Debug("debug");
			Log.Info("info");
			Log.Warn("warn");
			Log.Error("error");
			Log.Fatal("fatal");
		}

		private static void AdoNetAppenderProgrammaticConfiguration()
		{
			var adoNetAppender = new AdoNetAppender
			{
				ConnectionType = typeof(SqlConnection).AssemblyQualifiedName,
				ConnectionString = ConfigurationManager.ConnectionStrings["CritechRssStorage"].ConnectionString,
				BufferSize = 1,
				CommandText = "INSERT INTO Logs(Date, Thread, Level, Logger, Message, Exception) VALUES(@Date, @Thread, @Level, @Logger, @Message, @Exception)"
			};
			adoNetAppender.AddParameter(new AdoNetAppenderParameter
			{
				ParameterName = "@Date",
				DbType = DbType.DateTime,
				Layout = new RawTimeStampLayout()
			});
			adoNetAppender.AddParameter(new AdoNetAppenderParameter
			{
				ParameterName = "@Thread",
				DbType = DbType.String,
				Size = 255,
				Layout = new Layout2RawLayoutAdapter(new PatternLayout("%thread"))
			});
			adoNetAppender.AddParameter(new AdoNetAppenderParameter
			{
				ParameterName = "@Level",
				DbType = DbType.String,
				Size = 50,
				Layout = new Layout2RawLayoutAdapter(new PatternLayout("%level"))
			});
			adoNetAppender.AddParameter(new AdoNetAppenderParameter
			{
				ParameterName = "@Logger",
				DbType = DbType.String,
				Size = 255,
				Layout = new Layout2RawLayoutAdapter(new PatternLayout("%logger"))
			});
			adoNetAppender.AddParameter(new AdoNetAppenderParameter
			{
				ParameterName = "@Message",
				DbType = DbType.String,
				Size = 4000,
				Layout = new Layout2RawLayoutAdapter(new PatternLayout("%message"))
			});
			adoNetAppender.AddParameter(new AdoNetAppenderParameter
			{
				ParameterName = "@Exception",
				DbType = DbType.String,
				Size = 4000,
				Layout = new Layout2RawLayoutAdapter(new ExceptionLayout())
			});
			adoNetAppender.ActivateOptions();
			var hierarchy = (Hierarchy)LogManager.GetRepository();
			hierarchy.Root.AddAppender(adoNetAppender);
		}
	}
}
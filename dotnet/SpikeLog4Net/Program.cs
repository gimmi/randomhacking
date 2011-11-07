using System.Data;
using System.Data.SqlClient;
using log4net;
using log4net.Config;

namespace SpikeLog4Net
{
	public class Program
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

		private static void Main()
		{
			XmlConfigurator.Configure();

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
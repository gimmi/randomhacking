using System;
using NHibernate.Driver;

namespace NHibernate.Driver
{
	public class MonoSQLiteDriver : ReflectionBasedDriver
	{
		public MonoSQLiteDriver() : base("Mono.Data.Sqlite", 	"Mono.Data.Sqlite.SqliteConnection", "Mono.Data.Sqlite.SqliteCommand")
		{
		}
		
		public override bool UseNamedPrefixInSql
		{
			get { return true; }
		}
		
		public override bool UseNamedPrefixInParameter
		{
			get { return true; }
		}
		
		public override string NamedPrefix
		{
			get { return "@"; }
		}
		
		public override bool SupportsMultipleOpenReaders
		{
			get { return false; }
		}
	}
}

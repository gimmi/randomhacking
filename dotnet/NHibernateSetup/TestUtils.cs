using System.Data.SqlClient;

namespace NHibernateSetup
{
	public class TestUtils
	{
		/*
		 * Uses SQL server LocalDB
		 * Database file is located at: C:\Users\<user>\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\v11.0
		 * see http://msdn.microsoft.com/en-us/library/hh510202.aspx
		 * 
		 * For VS2010 you need to install:
		 * SQL Server 2012 express LocalDB
		 * http://support.microsoft.com/kb/2544514
		 */
		private const string ConnStrTemplate = @"Server=(localdb)\v11.0;Integrated Security=true;Initial Catalog={0}";
		private const string DbName = "NHibernateSetupTests";

		public static string ConnStr
		{
			get { return string.Format(ConnStrTemplate, DbName); }
		}

		public static void CreateTestDb()
		{
			SqlConnection.ClearAllPools();
			Execute(string.Concat("IF DB_ID('", DbName, "') IS NOT NULL DROP DATABASE ", DbName), "master");
			Execute(string.Concat("CREATE DATABASE ", DbName), "master");
		}

		public static void Execute(string sql, string dbName = DbName)
		{
			var conn = new SqlConnection(string.Format(ConnStrTemplate, dbName));
			conn.Open();
			try
			{
				new SqlCommand(sql, conn).ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
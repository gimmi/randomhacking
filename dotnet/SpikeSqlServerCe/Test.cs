using System;
using System.Data.SqlServerCe;
using System.IO;
using NUnit.Framework;

namespace SpikeSqlServerCe
{
	[TestFixture]
	public class Test
	{
		[Test]
		public void Tt()
		{
			string connectionString = @"DataSource=db.sdf";

			var conn = new SqlCeConnection(connectionString);
			if(!File.Exists(conn.Database))
			{
				new SqlCeEngine(connectionString).CreateDatabase();
			}
			conn.Open();

			//Creating a table
			var cmdCreate = new SqlCeCommand("CREATE TABLE Products (Id int IDENTITY(1,1), Title nchar(50), PRIMARY KEY(Id))", conn);
			cmdCreate.ExecuteNonQuery();

			//Inserting some data...
			var cmdInsert = new SqlCeCommand("INSERT INTO Products (Title) VALUES ('Some Product #1')", conn);
			cmdInsert.ExecuteNonQuery();

			//Making sure that our data was inserted by selecting it
			var cmdSelect = new SqlCeCommand("SELECT Id, Title FROM Products", conn);
			SqlCeDataReader reader = cmdSelect.ExecuteReader();
			reader.Read();
			Console.WriteLine("Id: {0} Title: {1}", reader["Id"], reader["Title"]);
			reader.Close();

			conn.Close();
		}
	}
}
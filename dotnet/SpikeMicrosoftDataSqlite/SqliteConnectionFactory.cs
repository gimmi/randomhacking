using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

namespace SpikeMicrosoftDataSqlite
{
    public class SqliteConnectionFactory
    {
        private readonly ILogger _logger;
        private readonly string _connectionString;

        public SqliteConnectionFactory(ILogger<SqliteConnectionFactory> logger, string connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        public SqliteConnection Open()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public void Migrate()
        {
            var migrations = new Dictionary<string, string> {
                ["001_names_table"] = "create table names(name text)",
            };
            
            using var connection =  Open();

            using var cmd = connection.CreateCommand();
            cmd.CommandText = "create table if not exists migrations(name text primary key) without rowid";
            cmd.Parameters.Clear();
            cmd.ExecuteNonQuery();

            foreach (var (name, sql) in migrations.OrderBy(x => x.Key))
            {
                cmd.CommandText = "select 1 from migrations where name = $name";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("$name", name);
                if (null == cmd.ExecuteScalar())
                {
                    _logger.LogInformation("Applying migration {}", name);
                    
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "insert into migrations(name) values($name)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("$name", name);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
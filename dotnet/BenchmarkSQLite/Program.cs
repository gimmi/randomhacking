using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace BenchmarkSQLite
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            File.Delete("mydb.sqlite");
            SQLiteConnection.CreateFile("mydb.sqlite");
            using (var conn = new SQLiteConnection("Data Source=mydb.sqlite;Version=3;"))
            {
                await conn.OpenAsync();
                var cmd = new SQLiteCommand("create table highscores (name varchar(20), score int)", conn);
                await cmd.ExecuteNonQueryAsync();

                using (var tr = conn.BeginTransaction())
                {
                    cmd = new SQLiteCommand("insert into highscores (name, score) values (@name, @Score)", conn);

                    var sw = Stopwatch.StartNew();
                    int count = 1_000_000;
                    for (int i = 0; i < count; i++)
                    {
                        cmd.Parameters.AddWithValue("name", "me");
                        cmd.Parameters.AddWithValue("score", i);
                        await cmd.ExecuteNonQueryAsync();
                    }
                    sw.Stop();
                    await Console.Out.WriteLineAsync($"{count/sw.Elapsed.TotalSeconds}");
                }
            }

            await Console.In.ReadLineAsync();
        }
    }
}

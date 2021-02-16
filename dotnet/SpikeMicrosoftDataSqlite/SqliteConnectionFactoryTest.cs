using System.Collections.Generic;
using NUnit.Framework;

namespace SpikeMicrosoftDataSqlite
{
    public class SqliteConnectionFactoryTest
    {
        [Test]
        public void Should_query()
        {
            using var conn = TestUtils.ConnFactory.Open();
            using var command = conn.CreateCommand();
            command.CommandText = "insert into names(name) values($name)";

            command.Parameters.AddWithValue("$name", "one");
            command.ExecuteNonQuery();

            command.Parameters["$name"].Value = "two";
            command.ExecuteNonQuery();

            command.Parameters["$name"].Value = "three";
            command.ExecuteNonQuery();


            var names = new List<string>();
            command.CommandText = "select name from names";
            command.Parameters.Clear();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                names.Add(reader.GetString(0));
            }
            
            Assert.That(names, Is.EquivalentTo(new[]{"one", "two", "three"}));
        }
    }
}
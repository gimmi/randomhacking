using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace SpikeMicrosoftDataSqlite
{
    [SetUpFixture]
    public class TestUtils
    {
        private static SqliteConnection _memoryDbConnection;
        public static SqliteConnectionFactory ConnFactory { get; private set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ConnFactory = new SqliteConnectionFactory(NullLogger<SqliteConnectionFactory>.Instance, "Data Source=Sharable;Mode=Memory;Cache=Shared");
            _memoryDbConnection = ConnFactory.Open();
            ConnFactory.Migrate();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _memoryDbConnection.Dispose();
        }
    }
}
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace NHibernateSetup
{
	[TestFixture]
	public class Test
	{
		private ISessionFactory _sessionFactory;

		[SetUp]
		public void SetUp()
		{
			Configuration cfg = BuildConfiguration();

			new SchemaExport(cfg).Create(false, true);

			_sessionFactory = cfg.BuildSessionFactory();
		}

		[Test]
		public void Tt()
		{
			var parent = new Parent();
			Assert.AreEqual(0, parent.RowVersion);
			using(ISession session = _sessionFactory.OpenSession())
			{
				using(session.BeginTransaction())
				{
					session.SaveOrUpdate(parent);
					Assert.AreEqual(1, parent.RowVersion);
					session.Transaction.Commit();
				}
			}
			using(ISession session = _sessionFactory.OpenSession())
			{
				using(session.BeginTransaction())
				{
					var parent1 = session.Load<Parent>(parent.Id);
					Assert.AreNotSame(parent, parent1);
					Assert.AreEqual(parent.Id, parent1.Id);
					Assert.AreEqual(parent.RowVersion, parent1.RowVersion);
					session.Transaction.Commit();
				}
			}
		}

		private static Configuration BuildConfiguration()
		{
			var cfg = new Configuration();
			// See http://fabiomaulo.blogspot.com/2009/07/nhibernate-configuration-through.html
			cfg.DataBaseIntegration(db => {
				db.Driver<SqlClientDriver>();
				db.Dialect<MsSql2008Dialect>();
				db.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Tests;Integrated Security=True"; // db.ConnectionStringName = "ConnStr";
				db.HqlToSqlSubstitutions = "true 1, false 0, yes 'Y', no 'N'";

				// See http://geekswithblogs.net/lszk/archive/2011/07/12/showing-a-sql-generated-by-nhibernate-on-the-vs-build-in.aspx
				db.LogSqlInConsole = true; // Remove if using Log4Net
				db.LogFormattedSql = true;
				db.AutoCommentSql = true;

				db.SchemaAction = SchemaAutoAction.Validate; // This correspond to "hbm2ddl.validate", see http://nhforge.org/blogs/nhibernate/archive/2008/11/23/nhibernate-hbm2ddl.aspx
			});

			var mapper = new ModelMapper();
			mapper.BeforeMapClass += (mi, type, map) => {
				map.Id(type.GetProperty("Id"), m => m.Generator(Generators.Assigned));
				map.Version(type.GetProperty("RowVersion"), m => m.UnsavedValue(0));
			};
			mapper.Class<Parent>(m => {
				m.Property(x => x.Description);
			});

			cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
			return cfg;
		}
	}
}
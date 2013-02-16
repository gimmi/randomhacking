using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using log4net.Config;

namespace NHibernateSetup
{
	[TestFixture]
	public class Test
	{
		[SetUp]
		public void SetUp()
		{
			TestUtils.CreateTestDb();
			Configuration cfg = NHibernateConfigurator.BuildConfiguration(TestUtils.ConnStr);

			new SchemaExport(cfg).Create(true, true);

			_sessionFactory = cfg.BuildSessionFactory();
		}

		private ISessionFactory _sessionFactory;

		[Test]
		public void Tt()
		{
			var parent = new Parent();
			Assert.AreEqual(0, parent.RowVersion);
			Assert.AreEqual(Guid.Empty, parent.Id);
			using (ISession session = _sessionFactory.OpenSession())
			{
				using (var tx = session.BeginTransaction())
				{
					session.SaveOrUpdate(parent);
					Assert.AreEqual(1, parent.RowVersion);
					Assert.AreNotEqual(Guid.Empty, parent.Id);
					tx.Commit();
				}
			}
			using (ISession session = _sessionFactory.OpenSession())
			{
				using (session.BeginTransaction())
				{
					var parent1 = session.Load<Parent>(parent.Id);
					Assert.AreNotSame(parent, parent1);
					Assert.AreEqual(parent.Id, parent1.Id);
					Assert.AreEqual(parent.RowVersion, parent1.RowVersion);
					session.Transaction.Commit();
				}
			}
		}
	}
}
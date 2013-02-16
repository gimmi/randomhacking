using System;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace NHibernateSetup
{
	public class NHibernateConfigurator
	{
		public static Configuration BuildConfiguration(string connStr)
		{
			var cfg = new Configuration();
			
			// See http://fabiomaulo.blogspot.com/2009/07/nhibernate-configuration-through.html
			cfg.DataBaseIntegration(db => {
				db.Driver<SqlClientDriver>();
				db.Dialect<MsSql2012Dialect>();
				db.ConnectionString = connStr; // db.ConnectionStringName = "ConnStr";
				db.HqlToSqlSubstitutions = "true 1, false 0, yes 'Y', no 'N'";

				// See http://geekswithblogs.net/lszk/archive/2011/07/12/showing-a-sql-generated-by-nhibernate-on-the-vs-build-in.aspx
				//db.LogSqlInConsole = true; // Remove if using Log4Net
				//db.LogFormattedSql = true;
				//db.AutoCommentSql = true;

				db.SchemaAction = SchemaAutoAction.Validate; // This correspond to "hbm2ddl.validate", see http://nhforge.org/blogs/nhibernate/archive/2008/11/23/nhibernate-hbm2ddl.aspx
			});

			var mapper = new ModelMapper();
			mapper.Class<Parent>(map => {
				map.Id(x => x.Id, m => {
					m.Generator(Generators.GuidComb);
					m.UnsavedValue(Guid.Empty);
				});
				map.Version(x => x.RowVersion, m => m.UnsavedValue(0));
				map.Property(x => x.Description);
			});
			cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
			return cfg;
		}
	}
}
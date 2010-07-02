using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data
{
	public class RegionRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public RegionRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.Region v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.Region Read(int regionId)
		{
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.Region>(regionId);
		}

		public void Update(ExtMvc.Domain.Region v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.Region v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.Region> SearchNormal(int? regionId, string regionDescription)
				{
					IQueryable<ExtMvc.Domain.Region> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.Region>();
								if(regionId != default(int?))
								{
									queryable = queryable.Where(x => x.RegionId == regionId);
								}
											if(!string.IsNullOrEmpty(regionDescription))
								{
									queryable = queryable.Where(x => x.RegionDescription == regionDescription);
								}
								
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.Region>(queryable);
				}
				
	}
}
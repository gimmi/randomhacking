using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data
{
	public class CustomerDemographicRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public CustomerDemographicRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.CustomerDemographic v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.CustomerDemographic Read(string customerTypeId)
		{
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.CustomerDemographic>(customerTypeId);
		}

		public void Update(ExtMvc.Domain.CustomerDemographic v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.CustomerDemographic v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.CustomerDemographic> SearchNormal(string customerTypeId, string customerDesc)
				{
					IQueryable<ExtMvc.Domain.CustomerDemographic> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.CustomerDemographic>();
								if(!string.IsNullOrEmpty(customerTypeId))
								{
									queryable = queryable.Where(x => x.CustomerTypeId == customerTypeId);
								}
											if(!string.IsNullOrEmpty(customerDesc))
								{
									queryable = queryable.Where(x => x.CustomerDesc == customerDesc);
								}
								
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.CustomerDemographic>(queryable);
				}
				
	}
}
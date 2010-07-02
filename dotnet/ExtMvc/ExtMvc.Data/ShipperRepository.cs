using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data
{
	public class ShipperRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public ShipperRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.Shipper v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.Shipper Read(int shipperId)
		{
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.Shipper>(shipperId);
		}

		public void Update(ExtMvc.Domain.Shipper v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.Shipper v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.Shipper> SearchNormal(int? shipperId, string companyName, string phone)
				{
					IQueryable<ExtMvc.Domain.Shipper> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.Shipper>();
								if(shipperId != default(int?))
								{
									queryable = queryable.Where(x => x.ShipperId == shipperId);
								}
											if(!string.IsNullOrEmpty(companyName))
								{
									queryable = queryable.Where(x => x.CompanyName == companyName);
								}
											if(!string.IsNullOrEmpty(phone))
								{
									queryable = queryable.Where(x => x.Phone == phone);
								}
								
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.Shipper>(queryable);
				}
				
	}
}
using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data
{
	public class SupplierRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public SupplierRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.Supplier v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.Supplier Read(int supplierId)
		{
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.Supplier>(supplierId);
		}

		public void Update(ExtMvc.Domain.Supplier v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.Supplier v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.Supplier> SearchNormal(int? supplierId, string companyName, string contactName, string contactTitle, string address, string city, string region, string postalCode, string country, string phone, string fax, string homePage)
				{
					IQueryable<ExtMvc.Domain.Supplier> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.Supplier>();
								if(supplierId != default(int?))
								{
									queryable = queryable.Where(x => x.SupplierId == supplierId);
								}
											if(!string.IsNullOrEmpty(companyName))
								{
									queryable = queryable.Where(x => x.CompanyName == companyName);
								}
											if(!string.IsNullOrEmpty(contactName))
								{
									queryable = queryable.Where(x => x.ContactName == contactName);
								}
											if(!string.IsNullOrEmpty(contactTitle))
								{
									queryable = queryable.Where(x => x.ContactTitle == contactTitle);
								}
											if(!string.IsNullOrEmpty(address))
								{
									queryable = queryable.Where(x => x.Address == address);
								}
											if(!string.IsNullOrEmpty(city))
								{
									queryable = queryable.Where(x => x.City == city);
								}
											if(!string.IsNullOrEmpty(region))
								{
									queryable = queryable.Where(x => x.Region == region);
								}
											if(!string.IsNullOrEmpty(postalCode))
								{
									queryable = queryable.Where(x => x.PostalCode == postalCode);
								}
											if(!string.IsNullOrEmpty(country))
								{
									queryable = queryable.Where(x => x.Country == country);
								}
											if(!string.IsNullOrEmpty(phone))
								{
									queryable = queryable.Where(x => x.Phone == phone);
								}
											if(!string.IsNullOrEmpty(fax))
								{
									queryable = queryable.Where(x => x.Fax == fax);
								}
											if(!string.IsNullOrEmpty(homePage))
								{
									queryable = queryable.Where(x => x.HomePage == homePage);
								}
								
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.Supplier>(queryable);
				}
				
	}
}
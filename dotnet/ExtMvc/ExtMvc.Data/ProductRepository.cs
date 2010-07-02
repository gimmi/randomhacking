using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data
{
	public class ProductRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public ProductRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.Product v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.Product Read(int productId)
		{
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.Product>(productId);
		}

		public void Update(ExtMvc.Domain.Product v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.Product v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.Product> SearchNormal(int? productId, string productName, bool? discontinued, ExtMvc.Domain.Category category, ExtMvc.Domain.Supplier supplier)
				{
					IQueryable<ExtMvc.Domain.Product> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.Product>();
								if(productId != default(int?))
								{
									queryable = queryable.Where(x => x.ProductId == productId);
								}
											if(!string.IsNullOrEmpty(productName))
								{
									queryable = queryable.Where(x => x.ProductName.StartsWith(productName));
								}
											if(discontinued != default(bool?))
								{
									queryable = queryable.Where(x => x.Discontinued == discontinued);
								}
											if(category != default(ExtMvc.Domain.Category))
								{
									queryable = queryable.Where(x => x.Category == category);
								}
											if(supplier != default(ExtMvc.Domain.Supplier))
								{
									queryable = queryable.Where(x => x.Supplier == supplier);
								}
								
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.Product>(queryable);
				}
				
	}
}
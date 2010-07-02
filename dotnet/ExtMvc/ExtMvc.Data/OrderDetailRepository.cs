using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data
{
	public class OrderDetailRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public OrderDetailRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.OrderDetail v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.OrderDetail Read(int orderId, int productId)
		{
			var keyObject = new ExtMvc.Domain.OrderDetail {OrderId = orderId, ProductId = productId};
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.OrderDetail>(keyObject);
		}

		public void Update(ExtMvc.Domain.OrderDetail v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.OrderDetail v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.OrderDetail> Search(int? orderId, int? productId, decimal? unitPrice, short? quantity, float? discount)
				{
					IQueryable<ExtMvc.Domain.OrderDetail> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.OrderDetail>();
								if(orderId != default(int?))
								{
									queryable = queryable.Where(x => x.OrderId == orderId);
								}
											if(productId != default(int?))
								{
									queryable = queryable.Where(x => x.ProductId == productId);
								}
											if(unitPrice != default(decimal?))
								{
									queryable = queryable.Where(x => x.UnitPrice == unitPrice);
								}
											if(quantity != default(short?))
								{
									queryable = queryable.Where(x => x.Quantity == quantity);
								}
											if(discount != default(float?))
								{
									queryable = queryable.Where(x => x.Discount == discount);
								}
								
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.OrderDetail>(queryable);
				}
				
	}
}
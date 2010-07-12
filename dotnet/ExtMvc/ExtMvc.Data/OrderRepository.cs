using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data
{
	public class OrderRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public OrderRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.Order v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.Order Read(int orderId)
		{
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.Order>(orderId);
		}

		public void Update(ExtMvc.Domain.Order v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.Order v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.Order> SearchNormal(int? orderId, System.DateTime? orderDate, System.DateTime? requiredDate, System.DateTime? shippedDate, decimal? freight, ExtMvc.Domain.Address address, ExtMvc.Domain.Ns.Customer customer, ExtMvc.Domain.Ns.Employee employee, ExtMvc.Domain.Shipper shipper)
				{
					IQueryable<ExtMvc.Domain.Order> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.Order>();
								if(orderId != default(int?))
								{
									queryable = queryable.Where(x => x.OrderId == orderId);
								}
											if(orderDate != default(System.DateTime?))
								{
									queryable = queryable.Where(x => x.OrderDate == orderDate);
								}
											if(requiredDate != default(System.DateTime?))
								{
									queryable = queryable.Where(x => x.RequiredDate == requiredDate);
								}
											if(shippedDate != default(System.DateTime?))
								{
									queryable = queryable.Where(x => x.ShippedDate == shippedDate);
								}
											if(freight != default(decimal?))
								{
									queryable = queryable.Where(x => x.Freight == freight);
								}
											if(address != default(ExtMvc.Domain.Address))
								{
									#warning Nexida.CodeGen.Warning: FieldName not specified in model, you have to manually implement filter
								}
											if(customer != default(ExtMvc.Domain.Ns.Customer))
								{
									queryable = queryable.Where(x => x.Customer == customer);
								}
											if(employee != default(ExtMvc.Domain.Ns.Employee))
								{
									queryable = queryable.Where(x => x.Employee == employee);
								}
											if(shipper != default(ExtMvc.Domain.Shipper))
								{
									queryable = queryable.Where(x => x.Shipper == shipper);
								}
								
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.Order>(queryable);
				}
				
	}
}
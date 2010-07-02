using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data
{
	public class CustomerRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public CustomerRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.Customer v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.Customer Read(string customerId)
		{
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.Customer>(customerId);
		}

		public void Update(ExtMvc.Domain.Customer v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.Customer v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.Customer> SearchNormal(string contactName)
				{
					IQueryable<ExtMvc.Domain.Customer> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.Customer>();
								if(!string.IsNullOrEmpty(contactName))
								{
									queryable = queryable.Where(x => x.ContactName.Contains(contactName));
								}
								
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.Customer>(queryable);
				}
				
	}
}
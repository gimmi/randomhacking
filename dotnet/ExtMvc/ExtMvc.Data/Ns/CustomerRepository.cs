using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data.Ns
{
	public class CustomerRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public CustomerRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.Ns.Customer v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.Ns.Customer Read(string customerId)
		{
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.Ns.Customer>(customerId);
		}

		public void Update(ExtMvc.Domain.Ns.Customer v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.Ns.Customer v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.Ns.Customer> SearchNormal(string contactName)
				{
					IQueryable<ExtMvc.Domain.Ns.Customer> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.Ns.Customer>();
								if(!string.IsNullOrEmpty(contactName))
								{
									queryable = queryable.Where(x => x.ContactName.Contains(contactName));
								}
								
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.Ns.Customer>(queryable);
				}
				
	}
}
using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data.Ns
{
	public class EmployeeRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public EmployeeRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.Ns.Employee v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.Ns.Employee Read(int employeeId)
		{
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.Ns.Employee>(employeeId);
		}

		public void Update(ExtMvc.Domain.Ns.Employee v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.Ns.Employee v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.Ns.Employee> SearchNormal()
				{
					IQueryable<ExtMvc.Domain.Ns.Employee> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.Ns.Employee>();
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.Ns.Employee>(queryable);
				}
				
	}
}
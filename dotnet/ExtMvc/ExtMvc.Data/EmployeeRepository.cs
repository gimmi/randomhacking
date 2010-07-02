using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data
{
	public class EmployeeRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public EmployeeRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.Employee v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.Employee Read(int employeeId)
		{
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.Employee>(employeeId);
		}

		public void Update(ExtMvc.Domain.Employee v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.Employee v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.Employee> SearchNormal()
				{
					IQueryable<ExtMvc.Domain.Employee> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.Employee>();
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.Employee>(queryable);
				}
				
	}
}
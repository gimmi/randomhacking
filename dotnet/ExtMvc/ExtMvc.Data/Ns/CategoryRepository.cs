using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data.Ns
{
	public class CategoryRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public CategoryRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.Ns.Category v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.Ns.Category Read(int categoryId)
		{
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.Ns.Category>(categoryId);
		}

		public void Update(ExtMvc.Domain.Ns.Category v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.Ns.Category v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.Ns.Category> SearchNormal()
				{
					IQueryable<ExtMvc.Domain.Ns.Category> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.Ns.Category>();
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.Ns.Category>(queryable);
				}
				
	}
}
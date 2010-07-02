using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data
{
	public class CategoryRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public CategoryRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.Category v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.Category Read(int categoryId)
		{
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.Category>(categoryId);
		}

		public void Update(ExtMvc.Domain.Category v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.Category v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.Category> SearchNormal()
				{
					IQueryable<ExtMvc.Domain.Category> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.Category>();
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.Category>(queryable);
				}
				
	}
}
using System.Linq;
using NHibernate.Linq;
using Nexida.Infrastructure;

namespace ExtMvc.Data
{
	public class TerritoryRepository : Nexida.Infrastructure.IRepository
	{

				private NHibernate.ISessionFactory _northwind;
				

		public TerritoryRepository(NHibernate.ISessionFactory northwind)	
		{

						_northwind = northwind;
						
		}
		
		public void Create(ExtMvc.Domain.Territory v)
		{
			_northwind.GetCurrentSession().Save(v);
		}

		public ExtMvc.Domain.Territory Read(string territoryId)
		{
			return _northwind.GetCurrentSession().Load<ExtMvc.Domain.Territory>(territoryId);
		}

		public void Update(ExtMvc.Domain.Territory v)
		{
			_northwind.GetCurrentSession().Update(v);
		}

		public void Delete(ExtMvc.Domain.Territory v)
		{
			_northwind.GetCurrentSession().Delete(v);
		}

				public IPresentableSet<ExtMvc.Domain.Territory> SearchNormal(string territoryDescription)
				{
					IQueryable<ExtMvc.Domain.Territory> queryable = _northwind.GetCurrentSession().Linq<ExtMvc.Domain.Territory>();
								if(!string.IsNullOrEmpty(territoryDescription))
								{
									queryable = queryable.Where(x => x.TerritoryDescription.StartsWith(territoryDescription));
								}
								
					return new Nexida.Infrastructure.QueryablePresentableSet<ExtMvc.Domain.Territory>(queryable);
				}
				
	}
}
using Nexida.Infrastructure;

namespace ExtMvc.Domain
{
	public class TerritoryFactory : IFactory<Territory>
	{
		public Territory Create()
		{
			return new Territory();
		}
	}
}
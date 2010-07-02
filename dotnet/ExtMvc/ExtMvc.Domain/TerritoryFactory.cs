namespace ExtMvc.Domain
{
	public class TerritoryFactory : Nexida.Infrastructure.IFactory<Territory>
	{
		public Territory Create()
		{
			return new Territory();
		}
	}
}
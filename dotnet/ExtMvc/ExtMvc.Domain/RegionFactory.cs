namespace ExtMvc.Domain
{
	public class RegionFactory : Nexida.Infrastructure.IFactory<Region>
	{
		public Region Create()
		{
			return new Region();
		}
	}
}
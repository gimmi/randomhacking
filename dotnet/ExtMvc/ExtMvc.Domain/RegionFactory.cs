using Nexida.Infrastructure;

namespace ExtMvc.Domain
{
	public class RegionFactory : IFactory<Region>
	{
		public Region Create()
		{
			return new Region();
		}
	}
}
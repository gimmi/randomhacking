using Nexida.Infrastructure;

namespace ExtMvc.Domain
{
	public class ShipperFactory : IFactory<Shipper>
	{
		public Shipper Create()
		{
			return new Shipper();
		}
	}
}
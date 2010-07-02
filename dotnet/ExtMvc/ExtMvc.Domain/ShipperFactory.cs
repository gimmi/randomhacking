namespace ExtMvc.Domain
{
	public class ShipperFactory : Nexida.Infrastructure.IFactory<Shipper>
	{
		public Shipper Create()
		{
			return new Shipper();
		}
	}
}
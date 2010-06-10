namespace ExtMvc.Domain
{
	public class OrderFactory : Nexida.Infrastructure.IFactory<Order>
	{
		public Order Create()
		{
			return new Order();
		}
	}
}
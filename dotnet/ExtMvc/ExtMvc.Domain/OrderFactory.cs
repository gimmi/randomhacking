using Nexida.Infrastructure;

namespace ExtMvc.Domain
{
	public class OrderFactory : IFactory<Order>
	{
		public Order Create()
		{
			return new Order();
		}
	}
}
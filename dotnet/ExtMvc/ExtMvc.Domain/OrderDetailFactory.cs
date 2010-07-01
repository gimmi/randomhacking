using Nexida.Infrastructure;

namespace ExtMvc.Domain
{
	public class OrderDetailFactory : IFactory<OrderDetail>
	{
		public OrderDetail Create()
		{
			return new OrderDetail();
		}
	}
}
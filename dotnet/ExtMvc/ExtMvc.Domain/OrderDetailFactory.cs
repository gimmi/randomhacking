namespace ExtMvc.Domain
{
	public class OrderDetailFactory : Nexida.Infrastructure.IFactory<OrderDetail>
	{
		public OrderDetail Create()
		{
			return new OrderDetail();
		}
	}
}
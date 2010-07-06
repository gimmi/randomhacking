using Nexida.Infrastructure;

namespace ExtMvc.Domain
{
	public class CustomerFactory : IFactory<Customer>
	{
		public Customer Create()
		{
			return new Customer();
		}
	}
}
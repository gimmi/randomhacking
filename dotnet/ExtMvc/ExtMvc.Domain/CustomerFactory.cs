namespace ExtMvc.Domain
{
	public class CustomerFactory : Nexida.Infrastructure.IFactory<Customer>
	{
		public Customer Create()
		{
			return new Customer();
		}
	}
}
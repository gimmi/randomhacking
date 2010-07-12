namespace ExtMvc.Domain.Ns
{
	public class CustomerFactory : Nexida.Infrastructure.IFactory<Customer>
	{
		public Customer Create()
		{
			return new Customer();
		}
	}
}
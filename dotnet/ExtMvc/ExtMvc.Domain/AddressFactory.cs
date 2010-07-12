namespace ExtMvc.Domain
{
	public class AddressFactory : Nexida.Infrastructure.IFactory<Address>
	{
		public Address Create()
		{
			return new Address();
		}
	}
}
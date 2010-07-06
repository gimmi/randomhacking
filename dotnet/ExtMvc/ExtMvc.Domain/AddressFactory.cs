using Nexida.Infrastructure;

namespace ExtMvc.Domain
{
	public class AddressFactory : IFactory<Address>
	{
		public Address Create()
		{
			return new Address();
		}
	}
}
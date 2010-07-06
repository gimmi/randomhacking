using Nexida.Infrastructure;

namespace ExtMvc.Domain
{
	public class CustomerDemographicFactory : IFactory<CustomerDemographic>
	{
		public CustomerDemographic Create()
		{
			return new CustomerDemographic();
		}
	}
}
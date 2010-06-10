namespace ExtMvc.Domain
{
	public class CustomerDemographicFactory : Nexida.Infrastructure.IFactory<CustomerDemographic>
	{
		public CustomerDemographic Create()
		{
			return new CustomerDemographic();
		}
	}
}
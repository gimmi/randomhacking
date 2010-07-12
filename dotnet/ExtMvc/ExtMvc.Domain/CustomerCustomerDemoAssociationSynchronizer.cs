namespace ExtMvc.Domain
{
	public static class CustomerCustomerDemoAssociationSynchronizer
	{
		public static void Associate(ExtMvc.Domain.Ns.Customer item1, ExtMvc.Domain.Ns.CustomerDemographic item2)
		{
			item1.Customerdemographics.Add(item2);
			item2.Customers.Add(item1);
		}

		public static void Disassociate(ExtMvc.Domain.Ns.Customer item1, ExtMvc.Domain.Ns.CustomerDemographic item2)
		{
			item1.Customerdemographics.Remove(item2);
			item2.Customers.Remove(item1);
		}

	}
}
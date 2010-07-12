namespace ExtMvc.Domain
{
	public static class FkOrdersCustomersAssociationSynchronizer
	{
		public static void Associate(ExtMvc.Domain.Order item1, ExtMvc.Domain.Ns.Customer item2)
		{
			if(item1.Customer != null)
			{
				item1.Customer.Orders.Remove(item1);
			}
			item1.Customer = item2;
			if(item1.Customer != null)
			{
				item1.Customer.Orders.Add(item1);
			}
		}

		public static void Disassociate(ExtMvc.Domain.Order item1, ExtMvc.Domain.Ns.Customer item2)
		{
			item1.Customer = null;
			item2.Orders.Remove(item1);
		}	}
}
namespace ExtMvc.Domain
{
	public static class FkOrdersShippersAssociationSynchronizer
	{
		public static void Associate(ExtMvc.Domain.Shipper item1, ExtMvc.Domain.Order item2)
		{
			if(item2.Shipper != null)
			{
				item2.Shipper.Orders.Remove(item2);
			}
			item2.Shipper = item1;
			if (item2.Shipper != null)
			{
				item1.Orders.Add(item2);
			}
		}

		public static void Disassociate(ExtMvc.Domain.Shipper item1, ExtMvc.Domain.Order item2)
		{
			item1.Orders.Remove(item2);
			item2.Shipper = null;
		}
	}
}
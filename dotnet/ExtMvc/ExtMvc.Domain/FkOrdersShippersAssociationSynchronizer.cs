namespace ExtMvc.Domain
{
	public static class FkOrdersShippersAssociationSynchronizer
	{
		public static void Associate(Shipper item1, Order item2)
		{
			if(item2.Shipper != null)
			{
				item2.Shipper.Orders.Remove(item2);
			}
			item2.Shipper = item1;
			if(item2.Shipper != null)
			{
				item1.Orders.Add(item2);
			}
		}

		public static void Disassociate(Shipper item1, Order item2)
		{
			item1.Orders.Remove(item2);
			item2.Shipper = null;
		}
	}
}
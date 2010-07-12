namespace ExtMvc.Domain
{
	public static class FkProductsSuppliersAssociationSynchronizer
	{
		public static void Associate(ExtMvc.Domain.Supplier item1, ExtMvc.Domain.Product item2)
		{
			if(item2.Supplier != null)
			{
				item2.Supplier.Products.Remove(item2);
			}
			item2.Supplier = item1;
			if (item2.Supplier != null)
			{
				item1.Products.Add(item2);
			}
		}

		public static void Disassociate(ExtMvc.Domain.Supplier item1, ExtMvc.Domain.Product item2)
		{
			item1.Products.Remove(item2);
			item2.Supplier = null;
		}
	}
}
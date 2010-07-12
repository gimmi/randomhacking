namespace ExtMvc.Domain
{
	public static class FkProductsCategoriesAssociationSynchronizer
	{
		public static void Associate(ExtMvc.Domain.Product item1, ExtMvc.Domain.Ns.Category item2)
		{
			if(item1.Category != null)
			{
				item1.Category.Products.Remove(item1);
			}
			item1.Category = item2;
			if(item1.Category != null)
			{
				item1.Category.Products.Add(item1);
			}
		}

		public static void Disassociate(ExtMvc.Domain.Product item1, ExtMvc.Domain.Ns.Category item2)
		{
			item1.Category = null;
			item2.Products.Remove(item1);
		}	}
}
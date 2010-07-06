namespace ExtMvc.Domain
{
	public static class FkProductsCategoriesAssociationSynchronizer
	{
		public static void Associate(Product item1, Category item2)
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

		public static void Disassociate(Product item1, Category item2)
		{
			item1.Category = null;
			item2.Products.Remove(item1);
		}
	}
}
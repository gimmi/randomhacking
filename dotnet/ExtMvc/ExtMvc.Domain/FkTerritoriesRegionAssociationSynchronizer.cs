namespace ExtMvc.Domain
{
	public static class FkTerritoriesRegionAssociationSynchronizer
	{
		public static void Associate(ExtMvc.Domain.Territory item1, ExtMvc.Domain.Region item2)
		{
			if(item1.Region != null)
			{
				item1.Region.Territories.Remove(item1);
			}
			item1.Region = item2;
			if(item1.Region != null)
			{
				item1.Region.Territories.Add(item1);
			}
		}

		public static void Disassociate(ExtMvc.Domain.Territory item1, ExtMvc.Domain.Region item2)
		{
			item1.Region = null;
			item2.Territories.Remove(item1);
		}	}
}
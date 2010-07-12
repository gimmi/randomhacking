namespace ExtMvc.Domain
{
	public static class EmployeeTerritoriesAssociationSynchronizer
	{
		public static void Associate(ExtMvc.Domain.Territory item1, ExtMvc.Domain.Ns.Employee item2)
		{
			item1.Employees.Add(item2);
			item2.Territories.Add(item1);
		}

		public static void Disassociate(ExtMvc.Domain.Territory item1, ExtMvc.Domain.Ns.Employee item2)
		{
			item1.Employees.Remove(item2);
			item2.Territories.Remove(item1);
		}

	}
}
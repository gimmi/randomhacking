namespace ExtMvc.Domain
{
	public static class EmployeeTerritoriesAssociationSynchronizer
	{
		public static void Associate(Territory item1, Employee item2)
		{
			item1.Employees.Add(item2);
			item2.Territories.Add(item1);
		}

		public static void Disassociate(Territory item1, Employee item2)
		{
			item1.Employees.Remove(item2);
			item2.Territories.Remove(item1);
		}
	}
}
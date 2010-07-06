namespace ExtMvc.Domain
{
	public static class FkOrdersEmployeesAssociationSynchronizer
	{
		public static void Associate(Order item1, Employee item2)
		{
			if(item1.Employee != null)
			{
				item1.Employee.Orders.Remove(item1);
			}
			item1.Employee = item2;
			if(item1.Employee != null)
			{
				item1.Employee.Orders.Add(item1);
			}
		}

		public static void Disassociate(Order item1, Employee item2)
		{
			item1.Employee = null;
			item2.Orders.Remove(item1);
		}
	}
}
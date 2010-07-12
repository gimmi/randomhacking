namespace ExtMvc.Domain
{
	public static class FkEmployeesEmployeesAssociationSynchronizer
	{
		public static void Associate(ExtMvc.Domain.Ns.Employee item1, ExtMvc.Domain.Ns.Employee item2)
		{
			if(item2.RelatedEmployee != null)
			{
				item2.RelatedEmployee.Employees.Remove(item2);
			}
			item2.RelatedEmployee = item1;
			if (item2.RelatedEmployee != null)
			{
				item1.Employees.Add(item2);
			}
		}

		public static void Disassociate(ExtMvc.Domain.Ns.Employee item1, ExtMvc.Domain.Ns.Employee item2)
		{
			item1.Employees.Remove(item2);
			item2.RelatedEmployee = null;
		}
	}
}
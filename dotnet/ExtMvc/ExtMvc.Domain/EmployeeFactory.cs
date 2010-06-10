namespace ExtMvc.Domain
{
	public class EmployeeFactory : Nexida.Infrastructure.IFactory<Employee>
	{
		public Employee Create()
		{
			return new Employee();
		}
	}
}
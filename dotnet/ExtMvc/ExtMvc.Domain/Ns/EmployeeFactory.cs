namespace ExtMvc.Domain.Ns
{
	public class EmployeeFactory : Nexida.Infrastructure.IFactory<Employee>
	{
		public Employee Create()
		{
			return new Employee();
		}
	}
}
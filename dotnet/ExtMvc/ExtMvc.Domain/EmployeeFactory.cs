using Nexida.Infrastructure;

namespace ExtMvc.Domain
{
	public class EmployeeFactory : IFactory<Employee>
	{
		public Employee Create()
		{
			return new Employee();
		}
	}
}
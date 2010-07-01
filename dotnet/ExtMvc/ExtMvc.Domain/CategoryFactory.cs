using Nexida.Infrastructure;

namespace ExtMvc.Domain
{
	public class CategoryFactory : IFactory<Category>
	{
		public Category Create()
		{
			return new Category();
		}
	}
}
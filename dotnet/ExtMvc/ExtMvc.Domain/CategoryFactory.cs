namespace ExtMvc.Domain
{
	public class CategoryFactory : Nexida.Infrastructure.IFactory<Category>
	{
		public Category Create()
		{
			return new Category();
		}
	}
}
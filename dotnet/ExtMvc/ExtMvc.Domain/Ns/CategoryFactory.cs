namespace ExtMvc.Domain.Ns
{
	public class CategoryFactory : Nexida.Infrastructure.IFactory<Category>
	{
		public Category Create()
		{
			return new Category();
		}
	}
}
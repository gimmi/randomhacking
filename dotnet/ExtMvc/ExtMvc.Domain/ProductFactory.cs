using Nexida.Infrastructure;

namespace ExtMvc.Domain
{
	public class ProductFactory : IFactory<Product>
	{
		public Product Create()
		{
			return new Product();
		}
	}
}
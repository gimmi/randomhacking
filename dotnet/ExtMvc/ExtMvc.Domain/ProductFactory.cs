namespace ExtMvc.Domain
{
	public class ProductFactory : Nexida.Infrastructure.IFactory<Product>
	{
		public Product Create()
		{
			return new Product();
		}
	}
}
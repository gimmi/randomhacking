using Nexida.Infrastructure;

namespace ExtMvc.Domain
{
	public class SupplierFactory : IFactory<Supplier>
	{
		public Supplier Create()
		{
			return new Supplier();
		}
	}
}
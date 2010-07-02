namespace ExtMvc.Domain
{
	public class SupplierFactory : Nexida.Infrastructure.IFactory<Supplier>
	{
		public Supplier Create()
		{
			return new Supplier();
		}
	}
}
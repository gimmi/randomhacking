namespace ExtMvc.Dtos
{
	public class ProductDto
	{
		public string StringId { get; set; }

		public int ProductId { get; set; }

		public string ProductName { get; set; }

		public string QuantityPerUnit { get; set; }

		public decimal? UnitPrice { get; set; }

		public short? UnitsInStock { get; set; }

		public short? UnitsOnOrder { get; set; }

		public short? ReorderLevel { get; set; }

		public bool Discontinued { get; set; }

		public CategoryReferenceDto Category { get; set; }

		// public ExtMvc.Dtos.SupplierReferenceDto Supplier { get; set; }
	}
}
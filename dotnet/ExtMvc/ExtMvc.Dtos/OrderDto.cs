using System;

namespace ExtMvc.Dtos
{
	public class OrderDto
	{
		public string StringId { get; set; }

		public int OrderId { get; set; }

		public DateTime? OrderDate { get; set; }

		public DateTime? RequiredDate { get; set; }

		public DateTime? ShippedDate { get; set; }

		public decimal? Freight { get; set; }

		public string ShipName { get; set; }

		public string ShipAddress { get; set; }

		public string ShipCity { get; set; }

		public string ShipRegion { get; set; }

		public string ShipPostalCode { get; set; }

		public string ShipCountry { get; set; }

		public CustomerReferenceDto Customer { get; set; }

		public EmployeeReferenceDto Employee { get; set; }

		// public ExtMvc.Dtos.ShipperReferenceDto Shipper { get; set; }
	}
}
﻿using System;

namespace JQueryMvc.Dtos
{
	public class OrderDto
	{
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
	}
}
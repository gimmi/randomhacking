namespace ExtMvc.Dtos
{
	public class TerritoryDto
	{
		public string StringId { get; set; }

		public string TerritoryId { get; set; }
				
		public string TerritoryDescription { get; set; }
				
		// public ExtMvc.Dtos.Ns.EmployeeReferenceDto[] Employees { get; set; }
				
		public ExtMvc.Dtos.RegionReferenceDto Region { get; set; }
				
	}
}
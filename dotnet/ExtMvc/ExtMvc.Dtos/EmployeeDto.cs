namespace ExtMvc.Dtos
{
	public class EmployeeDto
	{
		public string StringId { get; set; }

		public int EmployeeId { get; set; }
				
		public string LastName { get; set; }
				
		public string FirstName { get; set; }
				
		public string Title { get; set; }
				
		public string TitleOfCourtesy { get; set; }
				
		public System.DateTime? BirthDate { get; set; }
				
		public System.DateTime? HireDate { get; set; }
				
		public string Address { get; set; }
				
		public string City { get; set; }
				
		public string Region { get; set; }
				
		public string PostalCode { get; set; }
				
		public string Country { get; set; }
				
		public string HomePhone { get; set; }
				
		public string Extension { get; set; }
				
		// public byte[] Photo { get; set; }
				
		public string Notes { get; set; }
				
		public string PhotoPath { get; set; }
				
		// public ExtMvc.Dtos.EmployeeReferenceDto RelatedEmployee { get; set; }
				
		// public ExtMvc.Dtos.EmployeeReferenceDto[] Employees { get; set; }
				
		// public ExtMvc.Dtos.TerritoryReferenceDto[] Territories { get; set; }
				
		// public ExtMvc.Dtos.OrderReferenceDto[] Orders { get; set; }
				
	}
}
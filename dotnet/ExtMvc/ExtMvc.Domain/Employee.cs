namespace ExtMvc.Domain
{

	public class Employee 
	{

				private int _employeeId;
				
				private string _lastName;
				
				private string _firstName;
				
				private string _title;
				
				private string _titleOfCourtesy;
				
				private System.DateTime? _birthDate;
				
				private System.DateTime? _hireDate;
				
				private string _address;
				
				private string _city;
				
				private string _region;
				
				private string _postalCode;
				
				private string _country;
				
				private string _homePhone;
				
				private string _extension;
				
				private byte[] _photo;
				
				private string _notes;
				
				private string _photoPath;
				
				private ExtMvc.Domain.Employee _relatedEmployee;
				
				private System.Collections.Generic.ICollection<ExtMvc.Domain.Employee> _employees = new System.Collections.Generic.HashSet<ExtMvc.Domain.Employee>();
				
				private System.Collections.Generic.ICollection<ExtMvc.Domain.Territory> _territories = new System.Collections.Generic.HashSet<ExtMvc.Domain.Territory>();
				
				private System.Collections.Generic.ICollection<ExtMvc.Domain.Order> _orders = new System.Collections.Generic.HashSet<ExtMvc.Domain.Order>();
				

				public virtual int EmployeeId
				{ 
					get
					{
						return _employeeId;
					}
		set
					{
						_employeeId = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNullNotEmpty]
				public virtual string LastName
				{ 
					get
					{
						return _lastName;
					}
		set
					{
						_lastName = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNullNotEmpty]
				public virtual string FirstName
				{ 
					get
					{
						return _firstName;
					}
		set
					{
						_firstName = value;
					}
				}
				
				public virtual string Title
				{ 
					get
					{
						return _title;
					}
		set
					{
						_title = value;
					}
				}
				
				public virtual string TitleOfCourtesy
				{ 
					get
					{
						return _titleOfCourtesy;
					}
		set
					{
						_titleOfCourtesy = value;
					}
				}
				
				public virtual System.DateTime? BirthDate
				{ 
					get
					{
						return _birthDate;
					}
		set
					{
						_birthDate = value;
					}
				}
				
				public virtual System.DateTime? HireDate
				{ 
					get
					{
						return _hireDate;
					}
		set
					{
						_hireDate = value;
					}
				}
				
				public virtual string Address
				{ 
					get
					{
						return _address;
					}
		set
					{
						_address = value;
					}
				}
				
				public virtual string City
				{ 
					get
					{
						return _city;
					}
		set
					{
						_city = value;
					}
				}
				
				public virtual string Region
				{ 
					get
					{
						return _region;
					}
		set
					{
						_region = value;
					}
				}
				
				public virtual string PostalCode
				{ 
					get
					{
						return _postalCode;
					}
		set
					{
						_postalCode = value;
					}
				}
				
				public virtual string Country
				{ 
					get
					{
						return _country;
					}
		set
					{
						_country = value;
					}
				}
				
				public virtual string HomePhone
				{ 
					get
					{
						return _homePhone;
					}
		set
					{
						_homePhone = value;
					}
				}
				
				public virtual string Extension
				{ 
					get
					{
						return _extension;
					}
		set
					{
						_extension = value;
					}
				}
				
				public virtual byte[] Photo
				{ 
					get
					{
						return _photo;
					}
		set
					{
						_photo = value;
					}
				}
				
				public virtual string Notes
				{ 
					get
					{
						return _notes;
					}
		set
					{
						_notes = value;
					}
				}
				
				public virtual string PhotoPath
				{ 
					get
					{
						return _photoPath;
					}
		set
					{
						_photoPath = value;
					}
				}
				
				public virtual ExtMvc.Domain.Employee RelatedEmployee
				{ 
					get
					{
						return _relatedEmployee;
					}
		set
					{
						_relatedEmployee = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNull]
				public virtual System.Collections.Generic.ICollection<ExtMvc.Domain.Employee> Employees
				{ 
					get
					{
						return _employees;
					}
		private set
					{
						_employees = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNull]
				public virtual System.Collections.Generic.ICollection<ExtMvc.Domain.Territory> Territories
				{ 
					get
					{
						return _territories;
					}
		private set
					{
						_territories = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNull]
				public virtual System.Collections.Generic.ICollection<ExtMvc.Domain.Order> Orders
				{ 
					get
					{
						return _orders;
					}
		private set
					{
						_orders = value;
					}
				}
				
		public override string ToString()
		{
			return (_firstName == null ? "" : _firstName.ToString()) + " " + (_lastName == null ? "" : _lastName.ToString());
		}

				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj)) return true;
			var other = obj as Employee;
			if(ReferenceEquals(null, other)) return false;
			if (EmployeeId != default(int))
			{
				return other.EmployeeId == EmployeeId;
			}
			return base.Equals(obj);
		}
				
		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				if (EmployeeId != default(int))
				{
					result = (result * 397) ^ EmployeeId.GetHashCode();
				}
				else
				{
					result = base.GetHashCode();
				}
				return result;
			}
		}	

		
	}
}
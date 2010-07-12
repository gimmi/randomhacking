namespace ExtMvc.Domain.Ns
{

	public class Customer 
	{

				private string _customerId;
				
				private string _companyName;
				
				private string _contactName;
				
				private string _contactTitle;
				
				private string _address;
				
				private string _city;
				
				private string _region;
				
				private string _postalCode;
				
				private string _country;
				
				private string _phone;
				
				private string _fax;
				
				private System.Collections.Generic.ICollection<ExtMvc.Domain.Ns.CustomerDemographic> _customerdemographics = new System.Collections.Generic.HashSet<ExtMvc.Domain.Ns.CustomerDemographic>();
				
				private System.Collections.Generic.ICollection<ExtMvc.Domain.Order> _orders = new System.Collections.Generic.HashSet<ExtMvc.Domain.Order>();
				

				[NHibernate.Validator.Constraints.NotNullNotEmpty]
				public virtual string CustomerId
				{ 
					get
					{
						return _customerId;
					}
		set
					{
						_customerId = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNullNotEmpty]
				public virtual string CompanyName
				{ 
					get
					{
						return _companyName;
					}
		set
					{
						_companyName = value;
					}
				}
				
				public virtual string ContactName
				{ 
					get
					{
						return _contactName;
					}
		set
					{
						_contactName = value;
					}
				}
				
				public virtual string ContactTitle
				{ 
					get
					{
						return _contactTitle;
					}
		set
					{
						_contactTitle = value;
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
				
				public virtual string Phone
				{ 
					get
					{
						return _phone;
					}
		set
					{
						_phone = value;
					}
				}
				
				public virtual string Fax
				{ 
					get
					{
						return _fax;
					}
		set
					{
						_fax = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNull]
				public virtual System.Collections.Generic.ICollection<ExtMvc.Domain.Ns.CustomerDemographic> Customerdemographics
				{ 
					get
					{
						return _customerdemographics;
					}
		private set
					{
						_customerdemographics = value;
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
			return (_contactName == null ? "" : _contactName.ToString());
		}

				
				
				
				
				
				
				
				
				
				
				
				
				

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj)) return true;
			var other = obj as Customer;
			if(ReferenceEquals(null, other)) return false;
			if (CustomerId != default(string))
			{
				return other.CustomerId == CustomerId;
			}
			return base.Equals(obj);
		}
				
		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				if (CustomerId != default(string))
				{
					result = (result * 397) ^ CustomerId.GetHashCode();
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
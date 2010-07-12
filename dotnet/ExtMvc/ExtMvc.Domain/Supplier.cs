namespace ExtMvc.Domain
{

	public class Supplier 
	{

				private int _supplierId;
				
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
				
				private string _homePage;
				
				private System.Collections.Generic.ICollection<ExtMvc.Domain.Product> _products = new System.Collections.Generic.HashSet<ExtMvc.Domain.Product>();
				

				public virtual int SupplierId
				{ 
					get
					{
						return _supplierId;
					}
		set
					{
						_supplierId = value;
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
				
				public virtual string HomePage
				{ 
					get
					{
						return _homePage;
					}
		set
					{
						_homePage = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNull]
				public virtual System.Collections.Generic.ICollection<ExtMvc.Domain.Product> Products
				{ 
					get
					{
						return _products;
					}
		private set
					{
						_products = value;
					}
				}
				
		public override string ToString()
		{
			return (_companyName == null ? "" : _companyName.ToString());
		}

				
				
				
				
				
				
				
				
				
				
				
				
				

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj)) return true;
			var other = obj as Supplier;
			if(ReferenceEquals(null, other)) return false;
			if (SupplierId != default(int))
			{
				return other.SupplierId == SupplierId;
			}
			return base.Equals(obj);
		}
				
		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				if (SupplierId != default(int))
				{
					result = (result * 397) ^ SupplierId.GetHashCode();
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
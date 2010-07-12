namespace ExtMvc.Domain
{

	public class Shipper 
	{

				private int _shipperId;
				
				private string _companyName;
				
				private string _phone;
				
				private System.Collections.Generic.ICollection<ExtMvc.Domain.Order> _orders = new System.Collections.Generic.HashSet<ExtMvc.Domain.Order>();
				

				public virtual int ShipperId
				{ 
					get
					{
						return _shipperId;
					}
		set
					{
						_shipperId = value;
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
			return (_companyName == null ? "" : _companyName.ToString());
		}

				
				
				
				

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj)) return true;
			var other = obj as Shipper;
			if(ReferenceEquals(null, other)) return false;
			if (ShipperId != default(int))
			{
				return other.ShipperId == ShipperId;
			}
			return base.Equals(obj);
		}
				
		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				if (ShipperId != default(int))
				{
					result = (result * 397) ^ ShipperId.GetHashCode();
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
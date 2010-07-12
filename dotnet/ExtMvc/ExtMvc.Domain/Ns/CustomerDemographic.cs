namespace ExtMvc.Domain.Ns
{

	public class CustomerDemographic 
	{

				private string _customerTypeId;
				
				private string _customerDesc;
				
				private System.Collections.Generic.ICollection<ExtMvc.Domain.Ns.Customer> _customers = new System.Collections.Generic.HashSet<ExtMvc.Domain.Ns.Customer>();
				

				[NHibernate.Validator.Constraints.NotNullNotEmpty]
				public virtual string CustomerTypeId
				{ 
					get
					{
						return _customerTypeId;
					}
		set
					{
						_customerTypeId = value;
					}
				}
				
				public virtual string CustomerDesc
				{ 
					get
					{
						return _customerDesc;
					}
		set
					{
						_customerDesc = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNull]
				public virtual System.Collections.Generic.ICollection<ExtMvc.Domain.Ns.Customer> Customers
				{ 
					get
					{
						return _customers;
					}
		private set
					{
						_customers = value;
					}
				}
				
		public override string ToString()
		{
			return (_customerDesc == null ? "" : _customerDesc.ToString());
		}

				
				
				

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj)) return true;
			var other = obj as CustomerDemographic;
			if(ReferenceEquals(null, other)) return false;
			if (CustomerTypeId != default(string))
			{
				return other.CustomerTypeId == CustomerTypeId;
			}
			return base.Equals(obj);
		}
				
		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				if (CustomerTypeId != default(string))
				{
					result = (result * 397) ^ CustomerTypeId.GetHashCode();
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
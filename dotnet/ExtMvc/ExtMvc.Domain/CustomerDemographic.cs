using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace ExtMvc.Domain
{
	public class CustomerDemographic
	{
		private string _customerDesc;

		private ICollection<Customer> _customers = new HashSet<Customer>();


		[NotNullNotEmpty]
		public virtual string CustomerTypeId { get; set; }

		public virtual string CustomerDesc
		{
			get { return _customerDesc; }
			set { _customerDesc = value; }
		}

		[NotNull]
		public virtual ICollection<Customer> Customers
		{
			get { return _customers; }
			private set { _customers = value; }
		}

		public override string ToString()
		{
			return (_customerDesc == null ? "" : _customerDesc);
		}


		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			var other = obj as CustomerDemographic;
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(CustomerTypeId != default(string))
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
				if(CustomerTypeId != default(string))
				{
					result = (result*397) ^ CustomerTypeId.GetHashCode();
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
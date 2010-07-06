using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace ExtMvc.Domain
{
	public class Customer
	{
		private string _contactName;

		private ICollection<CustomerDemographic> _customerdemographics = new HashSet<CustomerDemographic>();

		private ICollection<Order> _orders = new HashSet<Order>();


		[NotNullNotEmpty]
		public virtual string CustomerId { get; set; }

		[NotNullNotEmpty]
		public virtual string CompanyName { get; set; }

		public virtual string ContactName
		{
			get { return _contactName; }
			set { _contactName = value; }
		}

		public virtual string ContactTitle { get; set; }

		public virtual string Address { get; set; }

		public virtual string City { get; set; }

		public virtual string Region { get; set; }

		public virtual string PostalCode { get; set; }

		public virtual string Country { get; set; }

		public virtual string Phone { get; set; }

		public virtual string Fax { get; set; }

		[NotNull]
		public virtual ICollection<CustomerDemographic> Customerdemographics
		{
			get { return _customerdemographics; }
			private set { _customerdemographics = value; }
		}

		[NotNull]
		public virtual ICollection<Order> Orders
		{
			get { return _orders; }
			private set { _orders = value; }
		}

		public override string ToString()
		{
			return (_contactName == null ? "" : _contactName);
		}


		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			var other = obj as Customer;
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(CustomerId != default(string))
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
				if(CustomerId != default(string))
				{
					result = (result*397) ^ CustomerId.GetHashCode();
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
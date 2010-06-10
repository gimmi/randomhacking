using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace ExtMvc.Domain
{
	public class Shipper
	{
		private string _companyName;

		private ICollection<Order> _orders = new HashSet<Order>();


		public virtual int ShipperId { get; set; }

		[NotNullNotEmpty]
		public virtual string CompanyName
		{
			get { return _companyName; }
			set { _companyName = value; }
		}

		public virtual string Phone { get; set; }

		[NotNull]
		public virtual ICollection<Order> Orders
		{
			get { return _orders; }
			private set { _orders = value; }
		}

		public override string ToString()
		{
			return (_companyName == null ? "" : _companyName);
		}


		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			var other = obj as Shipper;
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(ShipperId != default(int))
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
				if(ShipperId != default(int))
				{
					result = (result*397) ^ ShipperId.GetHashCode();
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
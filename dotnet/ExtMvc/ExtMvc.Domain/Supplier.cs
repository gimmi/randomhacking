using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace ExtMvc.Domain
{
	public class Supplier
	{
		private string _companyName;

		private ICollection<Product> _products = new HashSet<Product>();


		public virtual int SupplierId { get; set; }

		[NotNullNotEmpty]
		public virtual string CompanyName
		{
			get { return _companyName; }
			set { _companyName = value; }
		}

		public virtual string ContactName { get; set; }

		public virtual string ContactTitle { get; set; }

		public virtual string Address { get; set; }

		public virtual string City { get; set; }

		public virtual string Region { get; set; }

		public virtual string PostalCode { get; set; }

		public virtual string Country { get; set; }

		public virtual string Phone { get; set; }

		public virtual string Fax { get; set; }

		public virtual string HomePage { get; set; }

		[NotNull]
		public virtual ICollection<Product> Products
		{
			get { return _products; }
			private set { _products = value; }
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
			var other = obj as Supplier;
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(SupplierId != default(int))
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
				if(SupplierId != default(int))
				{
					result = (result*397) ^ SupplierId.GetHashCode();
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
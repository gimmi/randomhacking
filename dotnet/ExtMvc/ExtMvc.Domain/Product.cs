using NHibernate.Validator.Constraints;

namespace ExtMvc.Domain
{
	public class Product
	{
		private string _productName;


		public virtual int ProductId { get; set; }

		[NotNullNotEmpty]
		public virtual string ProductName
		{
			get { return _productName; }
			set { _productName = value; }
		}

		public virtual string QuantityPerUnit { get; set; }

		public virtual decimal? UnitPrice { get; set; }

		public virtual short? UnitsInStock { get; set; }

		public virtual short? UnitsOnOrder { get; set; }

		public virtual short? ReorderLevel { get; set; }

		public virtual bool Discontinued { get; set; }

		public virtual Category Category { get; set; }

		public virtual Supplier Supplier { get; set; }

		public override string ToString()
		{
			return (_productName == null ? "" : _productName);
		}


		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			var other = obj as Product;
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(ProductId != default(int))
			{
				return other.ProductId == ProductId;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				if(ProductId != default(int))
				{
					result = (result*397) ^ ProductId.GetHashCode();
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
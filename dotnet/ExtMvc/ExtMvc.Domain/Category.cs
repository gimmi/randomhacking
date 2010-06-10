using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace ExtMvc.Domain
{
	public class Category
	{
		private string _description;

		private ICollection<Product> _products = new HashSet<Product>();


		public virtual int CategoryId { get; set; }

		[NotNullNotEmpty]
		public virtual string CategoryName { get; set; }

		public virtual string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		public virtual byte[] Picture { get; set; }

		[NotNull]
		public virtual ICollection<Product> Products
		{
			get { return _products; }
			private set { _products = value; }
		}

		public override string ToString()
		{
			return (_description == null ? "" : _description);
		}


		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			var other = obj as Category;
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(CategoryId != default(int))
			{
				return other.CategoryId == CategoryId;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				if(CategoryId != default(int))
				{
					result = (result*397) ^ CategoryId.GetHashCode();
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
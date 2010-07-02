namespace ExtMvc.Domain
{

	public class Category 
	{

				private int _categoryId;
				
				private string _categoryName;
				
				private string _description;
				
				private byte[] _picture;
				
				private System.Collections.Generic.ICollection<ExtMvc.Domain.Product> _products = new System.Collections.Generic.HashSet<ExtMvc.Domain.Product>();
				

				public virtual int CategoryId
				{ 
					get
					{
						return _categoryId;
					}
		set
					{
						_categoryId = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNullNotEmpty]
				public virtual string CategoryName
				{ 
					get
					{
						return _categoryName;
					}
		set
					{
						_categoryName = value;
					}
				}
				
				public virtual string Description
				{ 
					get
					{
						return _description;
					}
		set
					{
						_description = value;
					}
				}
				
				public virtual byte[] Picture
				{ 
					get
					{
						return _picture;
					}
		set
					{
						_picture = value;
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
			return (_description == null ? "" : _description.ToString());
		}

				
				
				
				
				

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj)) return true;
			var other = obj as Category;
			if(ReferenceEquals(null, other)) return false;
			if (CategoryId != default(int))
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
				if (CategoryId != default(int))
				{
					result = (result * 397) ^ CategoryId.GetHashCode();
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
namespace ExtMvc.Domain
{

	public class Product 
	{

				private int _productId;
				
				private string _productName;
				
				private string _quantityPerUnit;
				
				private decimal? _unitPrice;
				
				private short? _unitsInStock;
				
				private short? _unitsOnOrder;
				
				private short? _reorderLevel;
				
				private bool _discontinued;
				
				private ExtMvc.Domain.Category _category;
				
				private ExtMvc.Domain.Supplier _supplier;
				

				public virtual int ProductId
				{ 
					get
					{
						return _productId;
					}
		set
					{
						_productId = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNullNotEmpty]
				public virtual string ProductName
				{ 
					get
					{
						return _productName;
					}
		set
					{
						_productName = value;
					}
				}
				
				public virtual string QuantityPerUnit
				{ 
					get
					{
						return _quantityPerUnit;
					}
		set
					{
						_quantityPerUnit = value;
					}
				}
				
				public virtual decimal? UnitPrice
				{ 
					get
					{
						return _unitPrice;
					}
		set
					{
						_unitPrice = value;
					}
				}
				
				public virtual short? UnitsInStock
				{ 
					get
					{
						return _unitsInStock;
					}
		set
					{
						_unitsInStock = value;
					}
				}
				
				public virtual short? UnitsOnOrder
				{ 
					get
					{
						return _unitsOnOrder;
					}
		set
					{
						_unitsOnOrder = value;
					}
				}
				
				public virtual short? ReorderLevel
				{ 
					get
					{
						return _reorderLevel;
					}
		set
					{
						_reorderLevel = value;
					}
				}
				
				public virtual bool Discontinued
				{ 
					get
					{
						return _discontinued;
					}
		set
					{
						_discontinued = value;
					}
				}
				
				public virtual ExtMvc.Domain.Category Category
				{ 
					get
					{
						return _category;
					}
		set
					{
						_category = value;
					}
				}
				
				public virtual ExtMvc.Domain.Supplier Supplier
				{ 
					get
					{
						return _supplier;
					}
		set
					{
						_supplier = value;
					}
				}
				
		public override string ToString()
		{
			return (_productName == null ? "" : _productName.ToString());
		}

				
				
				
				
				
				
				
				
				
				

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj)) return true;
			var other = obj as Product;
			if(ReferenceEquals(null, other)) return false;
			if (ProductId != default(int))
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
				if (ProductId != default(int))
				{
					result = (result * 397) ^ ProductId.GetHashCode();
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
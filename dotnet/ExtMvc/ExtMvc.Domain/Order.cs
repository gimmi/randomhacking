namespace ExtMvc.Domain
{

	public class Order 
	{

				private int _orderId;
				
				private System.DateTime? _orderDate;
				
				private System.DateTime? _requiredDate;
				
				private System.DateTime? _shippedDate;
				
				private decimal? _freight;
				
				private string _shipName;
				
				private string _shipAddress;
				
				private string _shipCity;
				
				private string _shipRegion;
				
				private string _shipPostalCode;
				
				private string _shipCountry;
				
				private ExtMvc.Domain.Ns.Customer _customer;
				
				private ExtMvc.Domain.Ns.Employee _employee;
				
				private ExtMvc.Domain.Shipper _shipper;
				

				public virtual int OrderId
				{ 
					get
					{
						return _orderId;
					}
		set
					{
						_orderId = value;
					}
				}
				
				public virtual System.DateTime? OrderDate
				{ 
					get
					{
						return _orderDate;
					}
		set
					{
						_orderDate = value;
					}
				}
				
				public virtual System.DateTime? RequiredDate
				{ 
					get
					{
						return _requiredDate;
					}
		set
					{
						_requiredDate = value;
					}
				}
				
				public virtual System.DateTime? ShippedDate
				{ 
					get
					{
						return _shippedDate;
					}
		set
					{
						_shippedDate = value;
					}
				}
				
				public virtual decimal? Freight
				{ 
					get
					{
						return _freight;
					}
		set
					{
						_freight = value;
					}
				}
				
				public virtual string ShipName
				{ 
					get
					{
						return _shipName;
					}
		set
					{
						_shipName = value;
					}
				}
				
				public virtual string ShipAddress
				{ 
					get
					{
						return _shipAddress;
					}
		set
					{
						_shipAddress = value;
					}
				}
				
				public virtual string ShipCity
				{ 
					get
					{
						return _shipCity;
					}
		set
					{
						_shipCity = value;
					}
				}
				
				public virtual string ShipRegion
				{ 
					get
					{
						return _shipRegion;
					}
		set
					{
						_shipRegion = value;
					}
				}
				
				public virtual string ShipPostalCode
				{ 
					get
					{
						return _shipPostalCode;
					}
		set
					{
						_shipPostalCode = value;
					}
				}
				
				public virtual string ShipCountry
				{ 
					get
					{
						return _shipCountry;
					}
		set
					{
						_shipCountry = value;
					}
				}
				
				public virtual ExtMvc.Domain.Ns.Customer Customer
				{ 
					get
					{
						return _customer;
					}
		set
					{
						_customer = value;
					}
				}
				
				public virtual ExtMvc.Domain.Ns.Employee Employee
				{ 
					get
					{
						return _employee;
					}
		set
					{
						_employee = value;
					}
				}
				
				public virtual ExtMvc.Domain.Shipper Shipper
				{ 
					get
					{
						return _shipper;
					}
		set
					{
						_shipper = value;
					}
				}
				
		public override string ToString()
		{
			return (_orderId == null ? "" : _orderId.ToString());
		}

				
				
				
				
				
				
				
				
				
				
				
				
				
				

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj)) return true;
			var other = obj as Order;
			if(ReferenceEquals(null, other)) return false;
			if (OrderId != default(int))
			{
				return other.OrderId == OrderId;
			}
			return base.Equals(obj);
		}
				
		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				if (OrderId != default(int))
				{
					result = (result * 397) ^ OrderId.GetHashCode();
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
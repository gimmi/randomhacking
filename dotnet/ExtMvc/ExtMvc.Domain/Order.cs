using System;

namespace ExtMvc.Domain
{
	public class Order
	{
		private int _orderId;


		public virtual int OrderId
		{
			get { return _orderId; }
			set { _orderId = value; }
		}

		public virtual DateTime? OrderDate { get; set; }

		public virtual DateTime? RequiredDate { get; set; }

		public virtual DateTime? ShippedDate { get; set; }

		public virtual decimal? Freight { get; set; }

		public virtual string ShipName { get; set; }

		public virtual string ShipAddress { get; set; }

		public virtual string ShipCity { get; set; }

		public virtual string ShipRegion { get; set; }

		public virtual string ShipPostalCode { get; set; }

		public virtual string ShipCountry { get; set; }

		public virtual Customer Customer { get; set; }

		public virtual Employee Employee { get; set; }

		public virtual Shipper Shipper { get; set; }

		public override string ToString()
		{
			return (_orderId == null ? "" : _orderId.ToString());
		}


		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			var other = obj as Order;
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(OrderId != default(int))
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
				if(OrderId != default(int))
				{
					result = (result*397) ^ OrderId.GetHashCode();
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
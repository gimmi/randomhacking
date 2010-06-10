using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ExtMvc.Domain
{
	public class OrderDetail : IXmlSerializable
	{
		private int _orderId;

		private int _productId;


		public virtual int OrderId
		{
			get { return _orderId; }
			set { _orderId = value; }
		}

		public virtual int ProductId
		{
			get { return _productId; }
			set { _productId = value; }
		}

		public virtual decimal UnitPrice { get; set; }

		public virtual short Quantity { get; set; }

		public virtual float Discount { get; set; }

		public override string ToString()
		{
			return (_orderId == null ? "" : _orderId.ToString()) + " " + (_productId == null ? "" : _productId.ToString());
		}


		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			var other = obj as OrderDetail;
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(OrderId != default(int) && ProductId != default(int))
			{
				return other.OrderId == OrderId && other.ProductId == ProductId;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				if(OrderId != default(int) && ProductId != default(int))
				{
					result = (result*397) ^ OrderId.GetHashCode();
					result = (result*397) ^ ProductId.GetHashCode();
				}
				else
				{
					result = base.GetHashCode();
				}
				return result;
			}
		}

		#region IXmlSerializable Members

		// Objects with composite keys must be serializable in order to work with NHibernate's lazy loading.
		// Only the key part need to be serialized.

		public virtual XmlSchema GetSchema()
		{
			return null;
		}

		public virtual void ReadXml(XmlReader reader)
		{
			reader.MoveToContent();
			reader.MoveToAttribute("OrderId");
			OrderId = reader.ReadContentAsInt();
			reader.MoveToAttribute("ProductId");
			ProductId = reader.ReadContentAsInt();
		}

		public virtual void WriteXml(XmlWriter writer)
		{
			writer.WriteStartAttribute("OrderId");
			writer.WriteValue(OrderId);
			writer.WriteEndAttribute();
			writer.WriteStartAttribute("ProductId");
			writer.WriteValue(ProductId);
			writer.WriteEndAttribute();
		}

		#endregion
	}
}
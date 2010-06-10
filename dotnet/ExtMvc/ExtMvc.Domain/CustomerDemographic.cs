using NHibernate.Validator.Constraints;

namespace ExtMvc.Domain
{
	public class CustomerDemographic
	{
		private string _customerDesc;


		[NotNullNotEmpty]
		public virtual string CustomerTypeId { get; set; }

		public virtual string CustomerDesc
		{
			get { return _customerDesc; }
			set { _customerDesc = value; }
		}

		public override string ToString()
		{
			return (_customerDesc == null ? "" : _customerDesc);
		}


		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			var other = obj as CustomerDemographic;
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(CustomerTypeId != default(string))
			{
				return other.CustomerTypeId == CustomerTypeId;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				if(CustomerTypeId != default(string))
				{
					result = (result*397) ^ CustomerTypeId.GetHashCode();
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
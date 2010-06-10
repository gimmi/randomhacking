using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace ExtMvc.Domain
{
	public class Region
	{
		private string _regionDescription;

		private ICollection<Territory> _territories = new HashSet<Territory>();


		public virtual int RegionId { get; set; }

		[NotNullNotEmpty]
		public virtual string RegionDescription
		{
			get { return _regionDescription; }
			set { _regionDescription = value; }
		}

		[NotNull]
		public virtual ICollection<Territory> Territories
		{
			get { return _territories; }
			private set { _territories = value; }
		}

		public override string ToString()
		{
			return (_regionDescription == null ? "" : _regionDescription);
		}


		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			var other = obj as Region;
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(RegionId != default(int))
			{
				return other.RegionId == RegionId;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				if(RegionId != default(int))
				{
					result = (result*397) ^ RegionId.GetHashCode();
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
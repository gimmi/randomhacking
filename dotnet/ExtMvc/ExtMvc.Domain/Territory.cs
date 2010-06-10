using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace ExtMvc.Domain
{
	public class Territory
	{
		private string _territoryDescription;

		private ICollection<Employee> _employees = new HashSet<Employee>();


		[NotNullNotEmpty]
		public virtual string TerritoryId { get; set; }

		[NotNullNotEmpty]
		public virtual string TerritoryDescription
		{
			get { return _territoryDescription; }
			set { _territoryDescription = value; }
		}

		[NotNull]
		public virtual ICollection<Employee> Employees
		{
			get { return _employees; }
			private set { _employees = value; }
		}

		[NotNull]
		public virtual Region Region { get; set; }

		public override string ToString()
		{
			return (_territoryDescription == null ? "" : _territoryDescription);
		}


		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			var other = obj as Territory;
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(TerritoryId != default(string))
			{
				return other.TerritoryId == TerritoryId;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				if(TerritoryId != default(string))
				{
					result = (result*397) ^ TerritoryId.GetHashCode();
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
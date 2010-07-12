namespace ExtMvc.Domain
{

	public class Territory 
	{

				private string _territoryId;
				
				private string _territoryDescription;
				
				private System.Collections.Generic.ICollection<ExtMvc.Domain.Ns.Employee> _employees = new System.Collections.Generic.HashSet<ExtMvc.Domain.Ns.Employee>();
				
				private ExtMvc.Domain.Region _region;
				

				[NHibernate.Validator.Constraints.NotNullNotEmpty]
				public virtual string TerritoryId
				{ 
					get
					{
						return _territoryId;
					}
		set
					{
						_territoryId = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNullNotEmpty]
				public virtual string TerritoryDescription
				{ 
					get
					{
						return _territoryDescription;
					}
		set
					{
						_territoryDescription = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNull]
				public virtual System.Collections.Generic.ICollection<ExtMvc.Domain.Ns.Employee> Employees
				{ 
					get
					{
						return _employees;
					}
		private set
					{
						_employees = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNull]
				public virtual ExtMvc.Domain.Region Region
				{ 
					get
					{
						return _region;
					}
		set
					{
						_region = value;
					}
				}
				
		public override string ToString()
		{
			return (_territoryDescription == null ? "" : _territoryDescription.ToString());
		}

				
				
				
				

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj)) return true;
			var other = obj as Territory;
			if(ReferenceEquals(null, other)) return false;
			if (TerritoryId != default(string))
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
				if (TerritoryId != default(string))
				{
					result = (result * 397) ^ TerritoryId.GetHashCode();
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
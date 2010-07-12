namespace ExtMvc.Domain
{

	public class Region 
	{

				private int _regionId;
				
				private string _regionDescription;
				
				private System.Collections.Generic.ICollection<ExtMvc.Domain.Territory> _territories = new System.Collections.Generic.HashSet<ExtMvc.Domain.Territory>();
				

				public virtual int RegionId
				{ 
					get
					{
						return _regionId;
					}
		set
					{
						_regionId = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNullNotEmpty]
				public virtual string RegionDescription
				{ 
					get
					{
						return _regionDescription;
					}
		set
					{
						_regionDescription = value;
					}
				}
				
				[NHibernate.Validator.Constraints.NotNull]
				public virtual System.Collections.Generic.ICollection<ExtMvc.Domain.Territory> Territories
				{ 
					get
					{
						return _territories;
					}
		private set
					{
						_territories = value;
					}
				}
				
		public override string ToString()
		{
			return (_regionDescription == null ? "" : _regionDescription.ToString());
		}

				
				
				

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj)) return true;
			var other = obj as Region;
			if(ReferenceEquals(null, other)) return false;
			if (RegionId != default(int))
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
				if (RegionId != default(int))
				{
					result = (result * 397) ^ RegionId.GetHashCode();
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
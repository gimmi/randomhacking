using System.Collections.Generic;

namespace SpikeExt4
{
	public class FilterClauseRepository
	{
		public IEnumerable<FilterClause> GetAll()
		{
			return new[]{
				new FilterClause{ Field = "Field 1", Operator = "=", Value = "1" },
				new FilterClause{ Field = "Field 2", Operator = "=", Value = "2" }
			};
		}
	}
}
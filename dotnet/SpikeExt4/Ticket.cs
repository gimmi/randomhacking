using System;
using System.Collections.Generic;

namespace SpikeExt4
{
	public class Ticket
	{
		public Guid Id = Guid.NewGuid();
		public string Title;
		public string Description;
		public string State;
		public IList<Comment> Comments = new List<Comment>();
	}
}
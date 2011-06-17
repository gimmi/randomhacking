using System;

namespace SpikeExt4
{
	public class Ticket
	{
		public Guid Id = Guid.NewGuid();
		public string Title;
		public string Description;
		public string State;
	}
}
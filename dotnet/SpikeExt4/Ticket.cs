using System;

namespace SpikeExt4
{
	public class Ticket
	{
		public Guid Id = Guid.NewGuid();
		public string Name;
		public string Email;
	}
}
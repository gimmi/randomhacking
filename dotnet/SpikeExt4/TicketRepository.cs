using System.Collections.Generic;

namespace SpikeExt4
{
	public class TicketRepository
	{
		public IEnumerable<Ticket> GetAll()
		{
			return new[]{
				new Ticket{ Name = "Ticket 1", Email = "1@1.com" },
				new Ticket{ Name = "Ticket 2", Email = "2@2.com" }
			};
		}
	}
}
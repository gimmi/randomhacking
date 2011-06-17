using System;
using System.Collections.Generic;

namespace SpikeExt4
{
	public class TicketRepository
	{
		public IEnumerable<Ticket> GetAll()
		{
			return new[]{
				new Ticket{ Title = "Ticket 1", Description = "t1", State = "Opened" },
				new Ticket{ Title = "Ticket 2", Description = "t2", State = "Closed" }
			};
		}

		public IEnumerable<TaskInfo> GetAllInfo()
		{
			return new[]{
				new TaskInfo{ Id = Guid.NewGuid(), Title = "Ticket 1" },
				new TaskInfo{ Id = Guid.NewGuid(), Title = "Ticket 2" }
			};
		}
	}
}
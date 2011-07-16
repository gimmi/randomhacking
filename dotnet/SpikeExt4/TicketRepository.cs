using System;
using System.Collections.Generic;
using NHibernate;

namespace SpikeExt4
{
	public class TicketRepository
	{
		private readonly ISession _session;

		public TicketRepository(ISession session)
		{
			_session = session;
		}

		public IEnumerable<Ticket> GetAll()
		{
			return new[] {
				new Ticket {
					Title = "Ticket 1", 
					Description = "t1", 
					State = "Opened", 
					Comments = new List<Comment> {
						new Comment { User = "Gimmi", Text = "Ok" },
						new Comment { User = "Elena", Text = "No!" },
					}
				},
				new Ticket {
					Title = "Ticket 2", 
					Description = "t2", 
					State = "Closed", 
					Comments = new List<Comment> {
						new Comment { User = "Gimmi", Text = "Ok" },
						new Comment { User = "Elena", Text = "No!" },
					}
				}
			};
		}

		public IEnumerable<TaskInfo> GetAllInfo()
		{
			return new[] {
				new TaskInfo { Id = Guid.NewGuid(), Title = "Ticket 1" },
				new TaskInfo { Id = Guid.NewGuid(), Title = "Ticket 2" }
			};
		}
	}
}
using System;
using System.Collections.Generic;
using NHibernate;
using log4net;

namespace SpikeExt4
{
	public class TicketRepository
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(TicketRepository));
		private readonly ISession _session;

		public TicketRepository(ISession session)
		{
			_session = session;
		}

		public IEnumerable<Ticket> Read()
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

		public void Create(Ticket ticket)
		{
			Log.Debug("Create");
		}

		public void Destroy(Ticket ticket)
		{
			Log.Debug("Destroy");
		}

		public void Update(Ticket ticket)
		{
			Log.Debug("Update");
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
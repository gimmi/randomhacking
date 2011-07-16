using System;

namespace SpikeExt4
{
	public class Comment
	{
		public Guid Id = Guid.NewGuid();
		public string User;
		public string Text;
	}
}
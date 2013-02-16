using System;

namespace NHibernateSetup
{
	public class Parent
	{
		public virtual Guid Id { get; set; }
		public virtual int RowVersion { get; set; }
		public virtual string Description { get; set; }
	}
}
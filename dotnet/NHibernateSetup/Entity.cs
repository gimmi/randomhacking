using System;

namespace NHibernateSetup
{
	public class Entity
	{
		private Guid _id = Guid.NewGuid();
		private int _rowVersion;

		public virtual Guid Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public virtual int RowVersion
		{
			get { return _rowVersion; }
			set { _rowVersion = value; }
		}

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj))
			{
				return false;
			}
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			if(obj.GetType() != typeof(Entity))
			{
				return false;
			}
			return ((Entity)obj).Id.Equals(Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
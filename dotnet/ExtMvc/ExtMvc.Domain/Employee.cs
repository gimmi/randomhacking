using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace ExtMvc.Domain
{
	public class Employee
	{
		private string _lastName;

		private string _firstName;

		private ICollection<Employee> _employees = new HashSet<Employee>();

		private ICollection<Territory> _territories = new HashSet<Territory>();

		private ICollection<Order> _orders = new HashSet<Order>();


		public virtual int EmployeeId { get; set; }

		[NotNullNotEmpty]
		public virtual string LastName
		{
			get { return _lastName; }
			set { _lastName = value; }
		}

		[NotNullNotEmpty]
		public virtual string FirstName
		{
			get { return _firstName; }
			set { _firstName = value; }
		}

		public virtual string Title { get; set; }

		public virtual string TitleOfCourtesy { get; set; }

		public virtual DateTime? BirthDate { get; set; }

		public virtual DateTime? HireDate { get; set; }

		public virtual string Address { get; set; }

		public virtual string City { get; set; }

		public virtual string Region { get; set; }

		public virtual string PostalCode { get; set; }

		public virtual string Country { get; set; }

		public virtual string HomePhone { get; set; }

		public virtual string Extension { get; set; }

		public virtual byte[] Photo { get; set; }

		public virtual string Notes { get; set; }

		public virtual string PhotoPath { get; set; }

		public virtual Employee RelatedEmployee { get; set; }

		[NotNull]
		public virtual ICollection<Employee> Employees
		{
			get { return _employees; }
			private set { _employees = value; }
		}

		[NotNull]
		public virtual ICollection<Territory> Territories
		{
			get { return _territories; }
			private set { _territories = value; }
		}

		[NotNull]
		public virtual ICollection<Order> Orders
		{
			get { return _orders; }
			private set { _orders = value; }
		}

		public override string ToString()
		{
			return (_firstName == null ? "" : _firstName) + " " + (_lastName == null ? "" : _lastName);
		}


		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			var other = obj as Employee;
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(EmployeeId != default(int))
			{
				return other.EmployeeId == EmployeeId;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				if(EmployeeId != default(int))
				{
					result = (result*397) ^ EmployeeId.GetHashCode();
				}
				else
				{
					result = base.GetHashCode();
				}
				return result;
			}
		}
	}
}
using System;
using NHibernate.Validator.Constraints;

namespace ExtMvc.Domain
{
	[Serializable]
	public class Address
	{
		private string _name;

		private string _addressString;

		private string _city;

		private string _region;

		private string _postalCode;

		private string _country;


		[NotNullNotEmpty]
		public virtual string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		[NotNullNotEmpty]
		public virtual string AddressString
		{
			get { return _addressString; }
			set { _addressString = value; }
		}

		[NotNullNotEmpty]
		public virtual string City
		{
			get { return _city; }
			set { _city = value; }
		}

		[NotNullNotEmpty]
		public virtual string Region
		{
			get { return _region; }
			set { _region = value; }
		}

		[NotNullNotEmpty]
		public virtual string PostalCode
		{
			get { return _postalCode; }
			set { _postalCode = value; }
		}

		[NotNullNotEmpty]
		public virtual string Country
		{
			get { return _country; }
			set { _country = value; }
		}

		public override string ToString()
		{
			return (_name == null ? "" : _name) + " " + (_addressString == null ? "" : _addressString) + " " + (_city == null ? "" : _city) + " " + (_region == null ? "" : _region) + " " + (_postalCode == null ? "" : _postalCode) + " " + (_country == null ? "" : _country);
		}


		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			var other = obj as Address;
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			return other.Name == Name && other.AddressString == AddressString && other.City == City && other.Region == Region && other.PostalCode == PostalCode && other.Country == Country;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = 0;
				result = (result*397) ^ ((Name != default(string)) ? Name.GetHashCode() : 0);
				result = (result*397) ^ ((AddressString != default(string)) ? AddressString.GetHashCode() : 0);
				result = (result*397) ^ ((City != default(string)) ? City.GetHashCode() : 0);
				result = (result*397) ^ ((Region != default(string)) ? Region.GetHashCode() : 0);
				result = (result*397) ^ ((PostalCode != default(string)) ? PostalCode.GetHashCode() : 0);
				result = (result*397) ^ ((Country != default(string)) ? Country.GetHashCode() : 0);
				return result;
			}
		}
	}
}
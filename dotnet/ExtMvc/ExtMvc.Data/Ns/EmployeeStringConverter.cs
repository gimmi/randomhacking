using System;

namespace ExtMvc.Data.Ns
{
	public class EmployeeStringConverter : Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Employee>
	{
		const char KeySeparator = '\\';
		private readonly ExtMvc.Data.Ns.EmployeeRepository _repository;

		public EmployeeStringConverter(ExtMvc.Data.Ns.EmployeeRepository repository)
		{
			_repository = repository;
		}

		public string ToString(ExtMvc.Domain.Ns.Employee obj)
		{
			return obj.EmployeeId.ToString();
		}

		public ExtMvc.Domain.Ns.Employee FromString(string str)
		{
			if(string.IsNullOrEmpty(str))
			{
				throw new ArgumentException("Must be a non null, non empty value", "str");
			}
			string[] keys = ParseKeys(str, 1);
			return _repository.Read(Convert.ToInt32(keys[0]));
		}
		
		/// <summary>
		/// Parses the keys.
		/// </summary>
		/// <param name="keyValues">The key values.</param>
		/// <param name="expectedNumberOfKeys">The expected number of keys.</param>
		/// <returns>The array containing the keys.</returns>
		public static string[] ParseKeys(string keyValues, int expectedNumberOfKeys)
		{
			string[] keys = keyValues.Split(KeySeparator);
			foreach (string key in keys)
			{
				if (key.Trim() == string.Empty)
					throw new ArgumentException("One of the provided keys is empty.", "keyValues");
			}

			if (keys.Length != expectedNumberOfKeys)
				throw new ArgumentException("The number of keys provided does not match the number of expected keys for this object.", "keyValues");

			return keys;
		}
	}
}
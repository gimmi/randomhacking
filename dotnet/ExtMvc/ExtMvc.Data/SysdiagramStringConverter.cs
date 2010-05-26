using System;
using ExtMvc.Domain;

namespace ExtMvc.Data
{
	public class SysdiagramStringConverter : Nexida.Infrastructure.IStringConverter<Sysdiagram>
	{
		private const char KeySeparator = '\\';
		private readonly SysdiagramRepository _repository;

		public SysdiagramStringConverter(SysdiagramRepository repository)
		{
			_repository = repository;
		}

		public string ToString(Sysdiagram obj)
		{
			return obj.DiagramId.ToString();
		}

		public Sysdiagram FromString(string str)
		{
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
			foreach(string key in keys)
			{
				if(key.Trim() == string.Empty)
				{
					throw new ArgumentException("One of the provided keys is empty.", "keyValues");
				}
			}

			if(keys.Length != expectedNumberOfKeys)
			{
				throw new ArgumentException("The number of keys provided does not match the number of expected keys for this object.", "keyValues");
			}

			return keys;
		}
	}
}
using System;

namespace ExtMvc.Data
{
	public class AddressStringConverter : Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Address>
	{

		public string ToString(ExtMvc.Domain.Address obj)
		{
			#warning Nexida.CodeGen.Warning: Here is a sample implementation relying on object serialization.
			var sb = new System.Text.StringBuilder();
			using (var stringWriter = new System.IO.StringWriter(sb))
			{
				new System.Xml.Serialization.XmlSerializer(typeof(ExtMvc.Domain.Address)).Serialize(stringWriter, obj);
			}
			return sb.ToString();
		}

		public ExtMvc.Domain.Address FromString(string str)
		{
			if(string.IsNullOrEmpty(str))
			{
				throw new ArgumentException("Must be a non null, non empty value", "str");
			}
			#warning Nexida.CodeGen.Warning: Here is an example implementation that relies on object serialization.
			var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(ExtMvc.Domain.Address));
			using (var stringReader = new System.IO.StringReader(str))
			{
				return (ExtMvc.Domain.Address)xmlSerializer.Deserialize(stringReader);
			}
		}
		
	}
}
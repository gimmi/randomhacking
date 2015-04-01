using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JsonParser
{
    public static class JsonParser
    {
		public static object Parse(TextReader reader)
		{
			reader.ConsumeWhitespace();

			if (reader.PeekCh() == '{')
			{
				// TODO call beginObject
				reader.ReadCh();
				reader.ConsumeWhitespace();
				while (reader.PeekCh() != '}')
				{
					while (true)
					{
						ParseString(reader);
						reader.ConsumeWhitespace();
						if (reader.ReadCh() != ':')
						{
							throw new Exception("Unexpected token");
						}
						Parse(reader);
						reader.ConsumeWhitespace();
						if (reader.PeekCh() != ',')
						{
							reader.ReadCh();
							break;
						}
					}
				}
				reader.ReadCh();
				// TODO call endObject
			}
			if (reader.PeekCh() == '[')
			{
				// TODO call beginArray
				reader.ReadCh();
				reader.ConsumeWhitespace();
				while (reader.PeekCh() != ']')
				{
					while (true)
					{
						Parse(reader);
						reader.ConsumeWhitespace();
						if (reader.PeekCh() != ',')
						{
							reader.ReadCh();
							break;
						}
					}
				}
				reader.ReadCh();
				// TODO call endArray
			}
			if (reader.PeekCh() == 'n')
			{
				if ("null" != new String(new[] { reader.ReadCh(), reader.ReadCh(), reader.ReadCh(), reader.ReadCh() }))
				{
					throw new Exception("Unexpected token");
				}
				// TODO call null
			}
			if (reader.PeekCh() == 't')
			{
				if ("true" != new String(new[] { reader.ReadCh(), reader.ReadCh(), reader.ReadCh(), reader.ReadCh() }))
				{
					throw new Exception("Unexpected token");
				}
				// TODO call true
			}
			if (reader.PeekCh() == 'f')
			{
				if ("false" != new String(new[] { reader.ReadCh(), reader.ReadCh(), reader.ReadCh(), reader.ReadCh() }))
				{
					throw new Exception("Unexpected token");
				}
				// TODO call false
			}
			if (reader.PeekCh() == '"')
			{
				return ParseString(reader);
			}
			if (reader.PeekCh() == '-' || char.IsDigit(reader.PeekCh()))
			{
				return ParseNumber(reader);
			}
			reader.ConsumeWhitespace();
			if (reader.Read() != -1)
			{
				throw new Exception("Unexpected token");
			}
		}

	    private static object ParseNumber(TextReader reader)
	    {
		    throw new NotImplementedException();
	    }

	    private static object ParseString(TextReader reader)
	    {
			reader.ConsumeWhitespace();
		    if (reader.ReadCh() != '"')
		    {
				throw new Exception("Unexpected token");
		    }
			var buf = new StringBuilder();
		    while (reader.PeekCh() != '"')
		    {
			    buf.Append(reader.ReadCh());
		    }
		    reader.ReadCh();
		    return buf.ToString();
	    }

		private static void ConsumeWhitespace(this TextReader reader)
	    {
			while (char.IsWhiteSpace(reader.PeekCh()))
			{
				reader.Read();
			}
	    }

		private static char PeekCh(this TextReader reader)
		{
			var ret = reader.Peek();
			if (ret == -1)
			{
				throw new Exception("Unexpected end of stream");
			}
			return (char) ret;
		}

		private static char ReadCh(this TextReader reader)
		{
			var ret = reader.Read();
			if (ret == -1)
			{
				throw new Exception("Unexpected end of stream");
			}
			return (char) ret;
		}
	}
}

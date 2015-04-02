using System;
using System.Collections.Generic;
using System.IO;
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
			    var dict = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
			    reader.NextCh();
                reader.ConsumeWhitespace();
				while (reader.PeekCh() != '}')
				{
					while (true)
					{
					    var key = ParseString(reader);
						reader.ConsumeWhitespace();
					    reader.ExpectStr(":");
                        dict.Add(key, Parse(reader));
						reader.ConsumeWhitespace();
						if (reader.PeekCh() != ',')
						{
                            reader.ExpectStr(",");
							break;
						}
					}
				}
                reader.ExpectStr("}");
			    return dict;
			}
			if (reader.PeekCh() == '[')
			{
                reader.NextCh();
                reader.ConsumeWhitespace();
                var list = new List<object>();
				while (reader.PeekCh() != ']')
				{
					while (true)
					{
						list.Add(Parse(reader));
						reader.ConsumeWhitespace();
						if (reader.PeekCh() != ',')
						{
							reader.NextCh();
							break;
						}
					}
				}
                reader.NextCh();
                return list;
			}
			if (reader.PeekCh() == 'n')
			{
                reader.ExpectStr("null");
			    return null;
			}
			if (reader.PeekCh() == 't')
			{
                reader.ExpectStr("true");
			    return true;
			}
			if (reader.PeekCh() == 'f')
			{
                reader.ExpectStr("false");
                return false;
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
	        var sb = new StringBuilder();
	        while (true)
	        {
	            var readCh = reader.PeekCh();
	        }
	    }

	    private static string ParseString(TextReader reader)
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

		private static void NextCh(this TextReader reader)
		{
			var ret = reader.Read();
			if (ret == -1)
			{
				throw new Exception("Unexpected end of stream");
			}
		}

		private static void ExpectStr(this TextReader reader, string str)
		{
            foreach (var ch in str)
		    {
                var ret = reader.Read();
                if (ret == -1)
                {
                    throw new Exception("Unexpected end of stream");
                }
                if (ch != (char)ret)
                {
                    throw new Exception("Expected char");
                }
		    }
		}
	}
}

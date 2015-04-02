using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace JsonParser
{
    public static class JsonParser
    {
        public static object Parse(TextReader rdr)
		{
			rdr.ConsumeWhitespace();
			if (rdr.PeekCh() == '{')
			{
			    var dict = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
			    rdr.Read();
                rdr.ConsumeWhitespace();
				while (rdr.PeekCh() != '}')
				{
					var key = ParseString(rdr);
					rdr.ConsumeWhitespace();
					rdr.ExpectStr(":");
					dict.Add(key, Parse(rdr));
					rdr.ConsumeWhitespace();
					if (rdr.PeekCh() == ',')
					{
						rdr.Read();
					}
				}
				rdr.Read();
			    return dict;
			}
			if (rdr.PeekCh() == '[')
			{
                rdr.Read();
                rdr.ConsumeWhitespace();
                var list = new List<object>();
				while (rdr.PeekCh() != ']')
				{
					list.Add(Parse(rdr));
					rdr.ConsumeWhitespace();
					if (rdr.PeekCh() == ',')
					{
						rdr.Read();
					}
				}
				rdr.Read();
                return list;
			}
			if (rdr.PeekCh() == 'n')
			{
                rdr.ExpectStr("null");
			    return null;
			}
			if (rdr.PeekCh() == 't')
			{
                rdr.ExpectStr("true");
			    return true;
			}
			if (rdr.PeekCh() == 'f')
			{
                rdr.ExpectStr("false");
                return false;
			}
			if (rdr.PeekCh() == '"')
			{
				return ParseString(rdr);
			}
			if (rdr.PeekCh() == '-' || char.IsDigit(rdr.PeekCh()))
			{
				var sb = new StringBuilder(rdr.ReadCh());
				while (rdr.PeekCh() == '+' || rdr.PeekCh() == '-' || rdr.PeekCh() == 'e' || rdr.PeekCh() == 'E' || char.IsDigit(rdr.PeekCh()))
				{
					sb.Append(rdr.ReadCh());
				}
				return decimal.Parse(sb.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture);
			}
	        throw new Exception();
		}

	    private static string ParseString(TextReader rdr)
	    {
			rdr.ConsumeWhitespace();
			rdr.ExpectStr("\"");
			var sb = new StringBuilder();
		    bool escaping = false;
		    while (true)
		    {
				if (!escaping && rdr.PeekCh() == '\\')
				{
					rdr.Read();
					escaping = true;
				}
				else if (escaping && rdr.PeekCh() == '\\')
				{
					rdr.Read();
					sb.Append('\\');
					escaping = false;
				}
				else if (escaping && rdr.PeekCh() == 'a')
				{
					rdr.Read();
					sb.Append('\a');
					escaping = false;
				}
				else if (escaping && rdr.PeekCh() == 'b')
				{
					rdr.Read();
					sb.Append('\b');
					escaping = false;
				}
				else if (escaping && rdr.PeekCh() == 'f')
				{
					rdr.Read();
					sb.Append('\f');
					escaping = false;
				}
				else if (escaping && rdr.PeekCh() == 'n')
				{
					rdr.Read();
					sb.Append('\n');
					escaping = false;
				}
				else if (escaping && rdr.PeekCh() == 'r')
				{
					rdr.Read();
					sb.Append('\r');
					escaping = false;
				}
				else if (escaping && rdr.PeekCh() == 't')
				{
					rdr.Read();
					sb.Append('\t');
					escaping = false;
				}
				else if (escaping && rdr.PeekCh() == 'v')
				{
					rdr.Read();
					sb.Append('\v');
					escaping = false;
				}
				else if (!escaping && rdr.PeekCh() == '"')
				{
					rdr.Read();
					return sb.ToString();
				}
				else if(rdr.Peek() == -1)
				{
					throw new Exception("Unexpected end of stream");
				}
				else
				{
					sb.Append(rdr.ReadCh());
				}
		    }
	    }

		private static void ConsumeWhitespace(this TextReader reader)
	    {
			while (char.IsWhiteSpace(reader.PeekCh()))
			{
				reader.Read();
			}
	    }

	    private static bool HasNext(this TextReader reader)
	    {
		    return reader.Peek() != -1;
	    }

		private static char PeekCh(this TextReader reader)
		{
			var ret = reader.Peek();
			if (ret == -1)
			{
				return '\0';
			}
			return (char) ret;
		}

		private static char ReadCh(this TextReader reader)
		{
			var ret = reader.Read();
			if (ret == -1)
			{
				return '\0';
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

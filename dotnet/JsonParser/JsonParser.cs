using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace JsonParser
{
    public static class JsonParser
    {
	    public static object Parse(string json)
	    {
		    return Parse(new StringReader(json));
	    }

        public static object Parse(TextReader en)
        {
            en.EatWhitespaces();
            var ch = en.PeekOrFail();
			if (ch == '{')
            {
	            en.Read();
                var dict = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
                en.EatWhitespaces();
                while (en.PeekOrFail() != '}')
                {
                    var key = ParseString(en);
                    en.EatWhitespaces();
					en.ReadExpected(":");
                    dict.Add(key, Parse(en));
                    en.EatWhitespaces();
                    if (en.PeekOrFail() == ',')
                    {
	                    en.Read();
						en.EatWhitespaces();
                    }
                }
				en.Read();
                return dict;
            }
            if (ch == '[')
            {
	            en.Read();
                var list = new List<object>();
                en.EatWhitespaces();
				while (en.PeekOrFail() != ']')
                {
                    list.Add(Parse(en));
                    en.EatWhitespaces();
                    if (en.PeekOrFail() == ',')
                    {
	                    en.Read();
						en.EatWhitespaces();
                    }
                }
	            en.Read();
                return list;
            }
            if (ch == 'n')
            {
				en.ReadExpected("null");
                return null;
            }
            if (ch == 't')
            {
				en.ReadExpected("true");
				return true;
            }
            if (ch == 'f')
            {
				en.ReadExpected("false");
				return false;
            }
            if (ch == '"')
            {
                return ParseString(en);
            }
            if (ch == '-' || char.IsDigit(ch))
            {
                var sb = new StringBuilder();
				while (ch == '+' || ch == '.' || ch == '-' || ch == 'e' || ch == 'E' || char.IsDigit(ch))
                {
                    sb.Append(en.ReadOrFail());
                    var peek = en.Peek();
                    if (peek == -1)
                    {
                        break;
                    }
                    ch = (char) peek;
                }
                decimal result;
                if (decimal.TryParse(sb.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                {
                    return result;
                }
				throw new FormatException(string.Format("Unable to parse '{0}' as number", sb));
            }
            throw new FormatException(string.Format("Unexpected char '{0}'", ch));
        }

        private static string ParseString(TextReader en)
        {
			en.EatWhitespaces();
	        var ch = en.ReadOrFail();
            if (ch != '"')
            {
                throw new FormatException(string.Format("Expected '\"', found '{0}'", ch));
            }
            var sb = new StringBuilder();
            while (true)
            {
                ch = en.ReadOrFail();
                if (ch == '\\')
                {
                    ch = en.ReadOrFail();
                    switch (ch)
                    {
                        case '\\':
                            sb.Append('\\');
                            break;
                        case 'a':
                            sb.Append('\a');
                            break;
                        case 'b':
                            sb.Append('\b');
                            break;
                        case 'f':
                            sb.Append('\f');
                            break;
                        case 'n':
                            sb.Append('\n');
                            break;
                        case 'r':
                            sb.Append('\r');
                            break;
                        case 't':
                            sb.Append('\t');
                            break;
                        case 'v':
                            sb.Append('\v');
                            break;
                        default:
                            throw new FormatException(string.Format(@"Unexpected escaped char \{0}", ch));
                    }
                }
                else if (ch == '"')
                {
                    return sb.ToString();
                }
                else
                {
                    sb.Append(ch);
                }
            }
        }

		public static void ReadExpected(this TextReader tr, string str)
		{
			foreach (var expected in str)
			{
				var actual = tr.ReadOrFail();
				if (actual != expected)
				{
					throw new FormatException(string.Format("Expected '{0}', found '{1}'", expected, actual));
				}
			}
		}

	    private static char ReadOrFail(this TextReader tr)
		{
			var read = tr.Read();
			if (read == -1)
			{
				throw new FormatException("Unexpected end of stream");
			}
			return (char)read;
		}

	    private static char PeekOrFail(this TextReader tr)
		{
			var peek = tr.Peek();
			if (peek == -1)
			{
				throw new FormatException("Unexpected end of stream");
			}
			return (char)peek;
		}

	    private static void EatWhitespaces(this TextReader tr)
		{
			var next = tr.Peek();
			while (next != -1 && char.IsWhiteSpace((char)next))
			{
				tr.Read();
				next = tr.Peek();
			}
		}
	}
}

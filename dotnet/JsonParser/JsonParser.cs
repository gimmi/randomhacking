using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace JsonParser
{
    public static class JsonParser
    {
        public class Tokenizer
        {
            private readonly IEnumerator<char> _en;
            private char? _last;

            public Tokenizer(IEnumerable<char> en)
            {
                _en = en.GetEnumerator();
                _last = null;
            }

            public bool MoveNext()
            {
                if (_last.HasValue)
                {
                    _last = null;
                    return true;
                }
                return _en.MoveNext();
            }

            public char Current
            {
                get { return _last ?? _en.Current; }
            }

            public void StepBack()
            {
                if (_last.HasValue)
                {
                    throw new JsonParserException("Cannot prepend more than once");
                }
                _last = _en.Current;
            }

            public void ConsumeWhitespace()
            {
                while (char.IsWhiteSpace(Current) && MoveNext())
                {
                }
            }

            public void MoveNextOrFail()
            {
                if (!MoveNext())
                {
                    throw new JsonParserException("Unexpected end of stream");
                }
            }

            public void ExpectStr(string str)
            {
                bool move = false;
                foreach (var ch in str)
                {
                    if (move)
                    {
                        MoveNextOrFail();
                    }
                    move = true;
                    if (Current != ch)
                    {
                        throw new JsonParserException("Expected '{0}', found '{1}'", ch, Current);
                    }
                }
            }
        }

        public static char ReadOrFail(this TextReader tr)
        {
            var read = tr.Read();
            if (read == -1)
            {
                throw new JsonParserException("");
            }
            return (char) read;
        }

        public static char PeekOrFail(this TextReader tr)
        {
            var peek = tr.Peek();
            if (peek == -1)
            {
                throw new JsonParserException("");
            }
            return (char) peek;
        }

        public static void EatWhitespaces(this TextReader tr)
        {
            var next = tr.Peek();
            while (next != -1 && char.IsWhiteSpace((char)next))
            {
                tr.Read();
                next = tr.Peek();
            }
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
                    if (en.ReadOrFail() != ':')
                    {
                        throw new JsonParserException("TODO");
                    }
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
				if (new String(new[] { en.ReadOrFail(), en.ReadOrFail(), en.ReadOrFail(), en.ReadOrFail() }) != "null")
                {
                    throw new JsonParserException("TODO");
                }
                return null;
            }
            if (ch == 't')
            {
				if (new String(new[] { en.ReadOrFail(), en.ReadOrFail(), en.ReadOrFail(), en.ReadOrFail() }) != "true")
                {
                    throw new JsonParserException("TODO");
                }
                return true;
            }
            if (ch == 'f')
            {
				if (new String(new[] { en.ReadOrFail(), en.ReadOrFail(), en.ReadOrFail(), en.ReadOrFail(), en.ReadOrFail() }) != "false")
                {
                    throw new JsonParserException("TODO");
                }
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
                throw new JsonParserException("Unable to parse '{0}' as number", sb.ToString());
            }
            throw new JsonParserException("Unexpected char '{0}'", ch);
        }

        private static string ParseString(TextReader en)
        {
			en.EatWhitespaces();
	        var ch = en.ReadOrFail();
            if (ch != '"')
            {
                throw new JsonParserException("TODO");
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
                            throw new JsonParserException(@"Unexpected escaped char \{0}", ch);
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

        private static void EatWhitespaces(this IEnumerator<char> en)
        {
            while (char.IsWhiteSpace(en.Current) && en.MoveNext())
            {
            }
        }

        private static void MoveNextOrFail(this IEnumerator<char> en)
        {
            if (!en.MoveNext())
            {
                throw new JsonParserException("Unexpected end of stream");
            }
        }

        private static void ExpectStr(this IEnumerator<char> en, string str)
        {
            bool move = false;
            foreach (var ch in str)
            {
                if (move)
                {
                    en.MoveNextOrFail();
                }
                move = true;
                if (en.Current != ch)
                {
                    throw new JsonParserException("Expected '{0}', found '{1}'", ch, en.Current);
                }
            }
        }
    }

    public class JsonParserException : Exception
    {
        public JsonParserException(string message)
            : base(message)
        {
        }

        public JsonParserException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}

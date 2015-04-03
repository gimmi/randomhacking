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

        public static char ReadCh(this TextReader tr)
        {
            var read = tr.Read();
            if (read == -1)
            {
                throw new JsonParserException("");
            }
            return (char) read;
        }

        public static char PeekCh(this TextReader tr)
        {
            var peek = tr.Peek();
            if (peek == -1)
            {
                throw new JsonParserException("");
            }
            return (char) peek;
        }

        public static void ConsumeWhitespace(this TextReader tr)
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
            en.ConsumeWhitespace();
            var ch = en.ReadCh();
            if (ch == '{')
            {
                var dict = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
                en.ConsumeWhitespace();
                ch = en.ReadCh();
                while (ch != '}')
                {
                    var key = ParseString(en.ReadCh(), en);
                    en.ConsumeWhitespace();
                    ch = en.ReadCh();
                    if (ch != ':')
                    {
                        throw new JsonParserException("TODO");
                    }
                    dict.Add(key, Parse(en));
                    en.ConsumeWhitespace();
                    ch = en.ReadCh();
                    if (ch != ',' && ch != '}')
                    {
                        throw new JsonParserException("TODO");
                    }
                }
                return dict;
            }
            if (ch == '[')
            {
                var list = new List<object>();
                en.ConsumeWhitespace();
                ch = en.ReadCh();
                while (ch != ']')
                {
                    list.Add(Parse(en));
                    en.ConsumeWhitespace();
                    ch = en.ReadCh();
                    if (ch != ',' && ch != '}')
                    {
                        throw new JsonParserException("TODO");
                    }
                }
                return list;
            }
            if (ch == 'n')
            {
                if (new String(new[] {ch, en.ReadCh(), en.ReadCh(), en.ReadCh()}) != "null")
                {
                    throw new JsonParserException("TODO");
                }
                return null;
            }
            if (ch == 't')
            {
                if (new String(new[] { ch, en.ReadCh(), en.ReadCh(), en.ReadCh() }) != "true")
                {
                    throw new JsonParserException("TODO");
                }
                return true;
            }
            if (ch == 'f')
            {
                if (new String(new[] { ch, en.ReadCh(), en.ReadCh(), en.ReadCh(), en.ReadCh() }) != "false")
                {
                    throw new JsonParserException("TODO");
                }
                return false;
            }
            if (ch == '"')
            {
                return ParseString(ch, en);
            }
            if (ch == '-' || char.IsDigit(ch))
            {
                var sb = new StringBuilder();
                while (true)
                {
                    sb.Append(ch);
                    var peek = en.Peek();
                    if (peek == -1)
                    {
                        break;
                    }
                    ch = (char) peek;
                    if (ch == '+' || ch == '.' || ch == '-' || ch == 'e' || ch == 'E' || char.IsDigit(ch))
                    {
                        en.Read();
                    }
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

        private static string ParseString(char ch, TextReader en)
        {
            if (ch != '"')
            {
                throw new JsonParserException("TODO");
            }
            var sb = new StringBuilder();
            while (true)
            {
                ch = en.ReadCh();
                if (ch == '\\')
                {
                    ch = en.ReadCh();
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

        private static void ConsumeWhitespace(this IEnumerator<char> en)
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

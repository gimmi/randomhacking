using System.IO;
using System.Reflection;
using System.Text;
using Jurassic;

namespace SpikeJurassic.UglifyJs
{
	public class Uglifier
	{
		public string Uglify(string script)
		{
			string uglifyCode = new StringBuilder()
				.AppendLine("var exports = {}; function require() { return exports; };")
				.AppendLine(ReadResource("parse-js.js"))
				.AppendLine(ReadResource("process.js"))
				.AppendLine("value = gen_code(ast_squeeze(ast_mangle(parse(value))));")
				.ToString();
			var engine = new ScriptEngine();
			engine.SetGlobalValue("value", script);
			engine.Execute(uglifyCode);
			return engine.GetGlobalValue<string>("value");
		}

		private string ReadResource(string fileName)
		{
			using(var s = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType(), fileName)))
			{
				return s.ReadToEnd();
			}
		}
	}
}
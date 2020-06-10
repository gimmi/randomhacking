using System;
using System.IO;
using System.Text;
using Jint;
using Jint.Native;

namespace SpikeJint
{
    public class Program
    {
        public static void Main()
        {
            var baseDir = Path.Combine(Directory.GetCurrentDirectory(), "test_dir");
            if (Directory.Exists(baseDir))
            {
                Directory.Delete(baseDir, true);
            }
            Directory.CreateDirectory(baseDir);
            File.WriteAllText(Path.Combine(baseDir, "file.json"), "{ a:1, b:2 /* comment */ }");
            File.WriteAllText(Path.Combine(baseDir, "file.js"), @"
                function prefix(val) {
                    return 'my' + val;
                }
            ");

            var input = @"{
                'aString': 'value',
                'aNumber': 3.14,
                'aBool': true,
                'aNull': null,
                'anArray': [
                    123,
                    '456'
                ],
                'anObj': {
                    'aString': 'value',
                    'aNumber': 3.14
                }
            }".Replace("'", "\"");

            var script = @"
                load('file.js');
                var file = load('file.json');

                output = {
                    myString: input.aString,
                    myArray: input.anArray.map(prefix),
                    file: file
                }                
            ";

            var output = TransformJson(script, input, baseDir);

            Console.Out.WriteLine(output);
        }

        private static string TransformJson(string script, string input, string baseDir)
        {
            var engine = new Engine();

            var jsApi = new JsApi(engine, baseDir);

            var inputJsValue = engine.Json.Parse(Undefined.Instance, new JsValue[] {engine.String.Construct(input)});
            var outputJsValue = engine
                .SetValue("load", new Func<JsValue, JsValue>(jsApi.Load))
                .SetValue("input", inputJsValue)
                .Execute(script)
                .GetValue("output");

            return engine.Json.Stringify(Undefined.Instance, new[] {outputJsValue, Undefined.Instance, engine.Number.Construct(2)}).ToString();
        }

        public class JsApi
        {
            private readonly Engine _engine;
            private readonly string _baseDir;

            public JsApi(Engine engine, string baseDir)
            {
                _engine = engine;
                _baseDir = baseDir;
            }

            public JsValue Load(JsValue value)
            {
                var path = value.AsString();
                if (!Path.IsPathRooted(path))
                {
                    path = Path.GetFullPath(path, _baseDir);
                }
                Console.Out.WriteLine("Loading " + path);
                var content = File.ReadAllText(path, Encoding.UTF8);
                if (Path.GetExtension(path) == ".json")
                {
                    // See https://stackoverflow.com/a/45016414/66629
                    content = string.Concat("(", content, ")");
                }
                return _engine.Execute(content).GetCompletionValue();
            }
        }
    }
}

using System;
using Jint;
using Jint.Native;

namespace SpikeJint
{
    public class Program
    {
        public static void Main()
        {
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
                output = {
                    myString: input.aString,
                    myArray: input.anArray.map(prefix)
                }

                function prefix(val) {
                    return 'my' + val;
                }
            ";

            var output = TransformJson(script, input);

            Console.Out.WriteLine(output);
        }

        private static string TransformJson(string script, string input)
        {
            var engine = new Engine();

            var inputJsValue = engine.Json.Parse(Undefined.Instance, new JsValue[] {engine.String.Construct(input)});

            var outputJsValue = engine.SetValue("input", inputJsValue).Execute(script).GetValue("output");

            return engine.Json.Stringify(Undefined.Instance, new[] {outputJsValue, Undefined.Instance, engine.Number.Construct(2)}).ToString();
        }
    }
}

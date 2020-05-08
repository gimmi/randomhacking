using System;
using System.Collections.Generic;
using Jint;
using Newtonsoft.Json;

namespace SpikeJint
{
    public class Program
    {
        public static void Main()
        {
            var input = new Dictionary<string, object> {
                ["aString"] = "value",
                ["aNumber"] = 3.14,
                ["aBool"] = true,
                ["aNull"] = null,
                ["anArray"] = new object[] {123, "456"},
                ["anObj"] = new Dictionary<string, object> {
                    ["aString"] = "value",
                    ["aNumber"] = 3.14
                }
            };

            var script = @"
                output = {
                    myString: input.aString,
                    myArray: input.anArray.map(prefix)
                }
                function prefix(val) {
                    return 'my' + val;
                }
            ";

            var output = new Engine()
                .SetValue("input", input)
                .SetValue("output", new Dictionary<string, object>())
                .Execute(script)
                .GetValue("output")
                .ToObject();

            var json = JsonConvert.SerializeObject(output, Formatting.Indented);

            Console.Out.WriteLine(json);
        }
    }
}

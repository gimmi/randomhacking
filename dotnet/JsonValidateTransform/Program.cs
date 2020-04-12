using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cocona;
using DotLiquid;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonValidateTransform
{
    public class Program
    {
        public static void Main(string[] args) => CoconaApp.Run<Program>(args);

        public async Task RunAsync([Option("in")] string inputDir, [Option("out")] string outputDir)
        {
            var modelPath = Path.Combine(inputDir, "model.json");
            var modelJson = await File.ReadAllTextAsync(modelPath);
            var model = JObject.Parse(modelJson);

            var schemaPath = Path.Combine(inputDir, "schema.json");
            var schemaJson = await File.ReadAllTextAsync(schemaPath);
            var schema = JSchema.Parse(schemaJson);

            var errors = new List<string>();
            model.Validate(schema, (_, e) => errors.Add(e.Message));
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    await Console.Error.WriteLineAsync(error);
                }

                return;
            }

            Directory.CreateDirectory(outputDir);
            foreach (var templatePath in Directory.GetFiles(inputDir, "*.liquid"))
            {
                var outputPath = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(templatePath) + ".json");
                await Console.Out.WriteLineAsync($"{Path.GetFileName(templatePath)} -> {Path.GetFileName(outputPath)}");
                var templateText = await File.ReadAllTextAsync(templatePath);
                var template = Template.Parse(templateText);

                var modelDict = model.ToObject<IDictionary<string, object>>();
                var modelHash = Hash.FromDictionary(modelDict);

                var outputRaw = template.Render(modelHash);
                var outputJson = JObject.Parse(outputRaw).ToString(Formatting.Indented);
                await File.WriteAllTextAsync(outputPath, outputJson);
            }
        }
    }
}

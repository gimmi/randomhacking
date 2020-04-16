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

            var schemaPath = Path.Combine(inputDir, model.Value<string>("$schema"));
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

                var modelHash = (Hash)JsonToHash(model);

                var outputText = template.Render(modelHash);
                var outputJson = JObject.Parse(outputText).ToString(Formatting.Indented);
                // var outputJson = outputText;
                await File.WriteAllTextAsync(outputPath, outputJson);
            }
        }

        private object JsonToHash(JToken jToken)
        {
            switch (jToken)
            {
                case JObject jObject:
                    return jObject.Properties().Aggregate(new Hash(), (hash, jProperty) => {
                        hash.Add(jProperty.Name, JsonToHash(jProperty.Value));
                        return hash;
                    });
                case JArray jArray:
                    return jArray.Select(JsonToHash).ToArray();
                case JValue jValue:
                    return jValue.Value;
                default:
                    throw new Exception("unknown");
            }
        }
    }
}

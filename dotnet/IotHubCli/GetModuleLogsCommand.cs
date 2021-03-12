using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Azure.Devices;

namespace IotHubCli
{
    [Verb("logs", HelpText = "Get module logs")]
    public class GetModuleLogsCommand : BaseCommand
    {
        [Option("module", Required = true, HelpText = "IoT module Id")]
        public string ModuleId { get; set; }

        public async Task<int> RunAsync()
        {
            var serviceClient = CreateClient();
            var request = new CloudToDeviceMethod("GetModuleLogs");
            request.SetPayloadJson(JsonSerializer.Serialize(new {
                schemaVersion = "1.0",
                contentType = "text",
                items = new[] {
                    new {
                        id = ModuleId,
                        filter = new {
                            tail = 100
                        }
                    }
                }
            }));
            var response = await serviceClient.InvokeDeviceMethodAsync(DeviceId, "$edgeAgent", request);
            using var jsonDocument = JsonDocument.Parse(response.GetPayloadAsJson());
            var payload = jsonDocument.RootElement
                .EnumerateArray()
                .First()
                .GetProperty("payload")
                .GetString();

            Console.WriteLine($"\nResponse status: {response.Status}, payload:\n\t{payload}");

            return 0;
        }
    }
}
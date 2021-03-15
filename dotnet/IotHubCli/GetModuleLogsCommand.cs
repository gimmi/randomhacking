using System;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IotHubCli
{
    [Verb("logs", HelpText = "Get module logs, for detailed info see https://docs.microsoft.com/en-us/azure/iot-edge/how-to-retrieve-iot-edge-logs#retrieve-module-logs")]
    public class GetModuleLogsCommand : BaseCommand
    {
        [Option("module", Required = true, HelpText = "IoT module Id")]
        public string ModuleId { get; set; } = "";

        [Option("tail", HelpText = "Number of log lines in the past to retrieve starting from the latest. Default to 100 if since not specified.")]
        public int Tail { get; set; } = 100;
        
        public async Task<int> RunAsync()
        {
            var serviceClient = CreateClient();
            var request = new CloudToDeviceMethod("GetModuleLogs");
            request.SetPayloadJson(JsonConvert.SerializeObject(new {
                schemaVersion = "1.0",
                contentType = "text",
                items = new[] {
                    new {
                        id = ModuleId,
                        filter = new {
                            tail = Tail
                        }
                    }
                }
            }));
            var response = await serviceClient.InvokeDeviceMethodAsync(DeviceId, "$edgeAgent", request);
            var payloadAsJson = response.GetPayloadAsJson();
            var logs = JArray.Parse(payloadAsJson)
                .Cast<JObject>()
                .Select(x => x.Value<string>("payload"));
            foreach (var log in logs)
            {
                await Console.Out.WriteLineAsync(log);
            }

            return 0;
        }
    }
}
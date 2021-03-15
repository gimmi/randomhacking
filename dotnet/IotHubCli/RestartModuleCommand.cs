using System;
using System.Text.Json;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Azure.Devices;

namespace IotHubCli
{
    [Verb("restart", HelpText = "Restart a module, for detailed info see https://docs.microsoft.com/en-us/azure/iot-edge/how-to-edgeagent-direct-method")]
    public class RestartModuleCommand : BaseCommand
    {
        [Option("module", Required = true, HelpText = "IoT module Id")]
        public string ModuleId { get; set; } = "";

        public async Task<int> RunAsync()
        {
            var serviceClient = CreateClient();
            var request = new CloudToDeviceMethod("RestartModule");
            request.SetPayloadJson(JsonSerializer.Serialize(new {
                schemaVersion = "1.0",
                id = ModuleId
            }));
            var response = await serviceClient.InvokeDeviceMethodAsync(DeviceId, "$edgeAgent", request);

            await Console.Out.WriteLineAsync($"Restart {DeviceId}/{ModuleId}: {response.Status}");

            return 0;
        }
    }
}
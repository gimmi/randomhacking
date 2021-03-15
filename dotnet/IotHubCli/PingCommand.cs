using System;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Azure.Devices;

namespace IotHubCli
{
    [Verb("ping", HelpText = "Ping a device, for detailed info see https://docs.microsoft.com/en-us/azure/iot-edge/how-to-edgeagent-direct-method")]
    public class PingCommand : BaseCommand
    {
        public async Task<int> RunAsync()
        {
            var serviceClient = CreateClient();
            var request = new CloudToDeviceMethod("ping");
            var response = await serviceClient.InvokeDeviceMethodAsync(DeviceId, "$edgeAgent", request);
            await Console.Out.WriteLineAsync($"Ping {DeviceId}: {response.Status}");
            return 0;
        }
    }
}
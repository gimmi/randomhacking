using System;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Azure.Devices;

namespace IotHubCli
{
    [Verb("ping", HelpText = "Ping a device")]
    public class PingCommand : BaseCommand
    {
        public async Task<int> RunAsync()
        {
            var serviceClient = CreateClient();
            var request = new CloudToDeviceMethod("ping");
            var response = await serviceClient.InvokeDeviceMethodAsync(DeviceId, "$edgeAgent", request);

            Console.WriteLine($"\nResponse status: {response.Status}, payload:\n\t{response.GetPayloadAsJson()}");

            return 0;
        }
    }
}
using CommandLine;
using Microsoft.Azure.Devices;

namespace IotHubCli
{
    public abstract class BaseCommand
    {
        [Option("conn", Required = true, HelpText = "IoT Hub connection string, get it with this command: az iot hub show-connection-string --hub-name my-hub --policy-name service")]
        public string Addr { get; set; } = "";

        [Option("device", Required = true, HelpText = "IoT device Id")]
        public string DeviceId { get; set; } = "";

        public ServiceClient CreateClient()
        {
            var csb = IotHubConnectionStringBuilder.Create(Addr);
            return ServiceClient.CreateFromConnectionString(csb.ToString());
        }
    }
}
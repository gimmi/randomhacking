using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpikeAzureEventHub
{
    public static class Program
    {
        public static CancellationTokenSource StopCts = new CancellationTokenSource();

        public static async Task Main(string[] args)
        {
            try
            {
                var connectionString = new EventHubsConnectionStringBuilder(args.First()) {
                    EntityPath = "TODO"
                }.ToString();

                await Console.Out.WriteLineAsync(connectionString);

                var eventHubClient = EventHubClient.CreateFromConnectionString(connectionString);
                BindCtrlC();

                do
                {
                    var message = new JObject {
                        {"id", Guid.NewGuid()},
                        {"datetime", DateTime.Now}
                    };
                    var messageJson = message.ToString(Formatting.None);
                    var messageBytes = Encoding.UTF8.GetBytes(messageJson);
                    await Console.Out.WriteLineAsync(messageJson);
                    await eventHubClient.SendAsync(new EventData(messageBytes));
                }
                while (await DelayAsync());

                Console.WriteLine("Done.");
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.ToString());
            }
        }

        private static void BindCtrlC()
        {
            Console.CancelKeyPress += (_, args) =>
            {
                args.Cancel = true;
                StopCts.Cancel();
            };
        }

        private static async Task<bool> DelayAsync()
        {
            try
            {
                await Task.Delay(1_000, StopCts.Token);
                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }
    }
}
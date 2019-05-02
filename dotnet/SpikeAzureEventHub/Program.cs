using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
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
                var sasKey = args.First();

                BindCtrlC();

                do
                {
                    var message = new JObject {
                        {"id", Guid.NewGuid()},
                        {"datetime", DateTime.Now}
                    };
                    var messageJson = message.ToString(Formatting.None);

                    await PublishWithHttpAsync(sasKey, messageJson);

//                    await PublishWithAmqpAsync(sasKey, messageJson);

                    await Console.Out.WriteLineAsync(messageJson);
                } while (await DelayAsync());

                Console.WriteLine("Done.");
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.ToString());
            }
        }

        // https://docs.microsoft.com/en-us/rest/api/eventhub/event-hubs-runtime-rest
        private static async Task PublishWithHttpAsync(string sasKey, string messageJson)
        {
            var resourceUri = "https://gimmidev.servicebus.windows.net/todo/messages";
            var keyName = "RootManageSharedAccessKey";

            var httpClient = new HttpClient {
                BaseAddress = new Uri(resourceUri),
                DefaultRequestHeaders = {
                    Authorization = CreateAuthorization(resourceUri, keyName, sasKey)
                }
            };
            
            var request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                Content = new StringContent(messageJson, Encoding.UTF8) {
                    Headers = {
                        ContentType = new MediaTypeHeaderValue("application/json") {
                            CharSet = Encoding.UTF8.WebName,
                            Parameters = {
                                new NameValueHeaderValue ("type", "entry")
                            }
                        }
                    }
                }
            };

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        private static AuthenticationHeaderValue CreateAuthorization(string resourceUri, string keyName, string key)
        {
            var expiry = (DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks) / 10_000_000;
            expiry += 60 * 60 * 24 * 7;
            var stringToSign = string.Format(CultureInfo.InvariantCulture, "{0}\n{1}", HttpUtility.UrlEncode(resourceUri), expiry);
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
            return new AuthenticationHeaderValue("SharedAccessSignature", string.Format(CultureInfo.InvariantCulture, "sr={0}&sig={1}&se={2}&skn={3}", HttpUtility.UrlEncode(resourceUri), HttpUtility.UrlEncode(signature), expiry, keyName));
        }

        private static async Task PublishWithAmqpAsync(string sasKey, string messageJson)
        {
            var csb = new EventHubsConnectionStringBuilder(new Uri("sb://gimmidev.servicebus.windows.net/"), "todo", "RootManageSharedAccessKey", sasKey);
            var eventHubClient = EventHubClient.Create(csb);
            var messageBytes = Encoding.UTF8.GetBytes(messageJson);
            var eventData = new EventData(messageBytes);
            await eventHubClient.SendAsync(eventData);
        }

        private static void BindCtrlC()
        {
            Console.CancelKeyPress += (_, args) => {
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
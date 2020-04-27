using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpikeAzureLogAnalyticsRest
{
    public class Program
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static async Task Main(string[] args)
        {
            var settings = JsonConvert.DeserializeObject<Settings>(@"{
                WorkspaceId: 'See azure-logs.jpg',
                SharedKey: 'See azure-logs.jpg',
                LogType: 'XOXOXOWWW',
                TimestampField: 'ts',
            }");

            var log = new JObject {
                { "ts", DateTime.UtcNow },
                { "a", "uno" },
                { "b", "due" },
            };
            await SendLogAsync(settings, log);
        }
        
        private static string HashSignature(string method, int contentLength, string contentType, string date, string resource, string logAnalyticsSharedKey)
        {
            var stringToSign = string.Join("\n",
                method,
                contentLength,
                contentType,
                "x-ms-date:" + date,
                resource
            );
            var bytesToSign = Encoding.UTF8.GetBytes(stringToSign);
            using var sha256 = new HMACSHA256(Convert.FromBase64String(logAnalyticsSharedKey));
            var calculatedHash = sha256.ComputeHash(bytesToSign);
            return Convert.ToBase64String(calculatedHash);
        }

        private static async Task SendLogAsync(Settings settings, JObject log)
        {
            // https://docs.microsoft.com/en-us/rest/api/loganalytics/create-request
            
            var resource = "/api/logs";
            var xMsDate = DateTime.UtcNow.ToString("R");
            var contentType = "application/json";
            var httpMethod = HttpMethod.Post;

            var content = Encoding.UTF8.GetBytes(log.ToString());

            var signature = HashSignature(httpMethod.Method, content.Length, contentType, xMsDate, resource, settings.SharedKey);

            var request = new HttpRequestMessage {
                Method = httpMethod,
                RequestUri = new Uri($"https://{settings.WorkspaceId}.ods.opinsights.azure.com{resource}?api-version=2016-04-01"),
                Headers = {
                    { "Authorization", string.Concat("SharedKey ", settings.WorkspaceId, ":", signature) },
                    { "x-ms-date", xMsDate },
                    { "Log-Type", settings.LogType },
                    { "time-generated-field", settings.TimestampField },
                },
                Content = new ByteArrayContent(content) {
                    Headers = {
                        ContentType = new MediaTypeHeaderValue(contentType)
                    }
                }
            };
                
            Console.WriteLine($"{request.Method} {request.RequestUri}");

            var response = await HttpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public class Settings
        {
            public string WorkspaceId { get; set; }
            public string SharedKey { get; set; }

            public string LogType { get; set; }
            public string TimestampField { get; set; }
        }
    }
}

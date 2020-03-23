using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpikeAzureStorageRest
{
    public class Program
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static async Task Main(string[] args)
        {
            // Output from the command
            // az ad sp create-for-rbac --name http://my_serviceprincipal --skip-assignment
            var servicePrincipal = JsonConvert.DeserializeObject<ServicePrincipal>(@"{
              appId: 'TODO',
              password: 'TODO',
              tenant: 'TODO'
            }");

            var bytes = await GetBlobAsync(servicePrincipal, "mdlreleases", "configurations", "/rig2/my-module.json");
            if (bytes.Length == 0)
            {
                Console.WriteLine("Not found");
            }
            else
            {
                var str = Encoding.UTF8.GetString(bytes);
                Console.WriteLine(str);
            }
        }

        private static async Task<string> GetAccessTokenAsync(ServicePrincipal servicePrincipal, string resource)
        {
            // See https://docs.microsoft.com/en-us/azure/active-directory/azuread-dev/v1-oauth2-client-creds-grant-flow
            var request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://login.microsoftonline.com/{Uri.EscapeDataString(servicePrincipal.Tenant)}/oauth2/token"),
                Content = new FormUrlEncodedContent(new Dictionary<string, string> {
                    ["grant_type"] = "client_credentials",
                    ["client_id"] = servicePrincipal.AppId,
                    ["client_secret"] = servicePrincipal.Password,
                    ["resource"] = resource
                })
            };
            var response = await HttpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var jsonContent = await response.Content.ReadAsStringAsync();
            var auth = JsonConvert.DeserializeObject<JObject>(jsonContent);
            var accessToken = auth.GetValue("access_token").Value<string>();
            return accessToken;
        }

        private static async Task<byte[]> GetBlobAsync(ServicePrincipal servicePrincipal, string storageAccountName, string containerName, string blobPath)
        {
            var resource = $"https://{storageAccountName}.blob.core.windows.net/";

            // This require the "Storage Blob Data Reader" role on the service principal
            var accessToken = await GetAccessTokenAsync(servicePrincipal, resource);

            var uri = string.Concat(resource, containerName, blobPath);

            var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers = {
                    Authorization = new AuthenticationHeaderValue("Bearer", accessToken),
                    Date = DateTimeOffset.UtcNow
                }
            };

            Console.WriteLine($"{request.Method} {request.RequestUri}");

            request.Headers.Add("x-ms-version", "2019-07-07");

            var response = await HttpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new byte[0];
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }

        public class ServicePrincipal
        {
            public string AppId { get; set; }
            public string Password { get; set; }
            public string Tenant { get; set; }
        }
    }
}

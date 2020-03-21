using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
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

            await ListConteinersAsync(servicePrincipal);
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

        private static async Task ListConteinersAsync(ServicePrincipal servicePrincipal)
        {
            var storageAccountName = "mysa4";
            var resource = $"https://{storageAccountName}.blob.core.windows.net/";

            // This require the "Storage Blob Data Reader" role on the service principal
            var accessToken = await GetAccessTokenAsync(servicePrincipal, resource);

            var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{Uri.EscapeDataString(storageAccountName)}.blob.core.windows.net/?comp=list"),
                Headers = {
                    Authorization = new AuthenticationHeaderValue("Bearer", accessToken),
                    Date = DateTimeOffset.UtcNow
                }
            };

            request.Headers.Add("x-ms-version", "2019-07-07");

            var response = await HttpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var xmlContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(xmlContent);

            var xElement = XElement.Parse(xmlContent);
            foreach (var container in xElement.Element("Containers").Elements("Container"))
            {
                Console.WriteLine("Container name = {0}", container.Element("Name").Value);
            }
        }

        public class ServicePrincipal
        {
            public string AppId { get; set; }
            public string Password { get; set; }
            public string Tenant { get; set; }
        }
    }
}

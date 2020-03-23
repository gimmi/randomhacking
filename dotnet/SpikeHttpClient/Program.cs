using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SpikeHttpClient
{
    public class Program
    {
        public class AppConfig
        {
            public string RemoteUri { get; set; }
            public string Proxy { get; set; }
            public string[] Certs { get; set; } = new string[0];
        }

        public static async Task Main(string[] args)
        {
            var appConfig = new AppConfig {
                RemoteUri = "https://stackoverflow.com",
                Proxy = "http://my.proxy",
                Certs = new[] {
                    // Base-64 encoded X.509 (.CER)
                    "MIIDujCCAqKgAwIBAgILBAAAAAABD4Ym5g0wDQYJKoZIhvcNAQEFBQAwTDEgMB4GA1UECxMXR2xvYmFsU2lnbiBSb290IENBIC0gUjIxEzARBgNVBAoTCkdsb2JhbFNpZ24xEzARBgNVBAMTCkdsb2JhbFNpZ24wHhcNMDYxMjE1MDgwMDAwWhcNMjExMjE1MDgwMDAwWjBMMSAwHgYDVQQLExdHbG9iYWxTaWduIFJvb3QgQ0EgLSBSMjETMBEGA1UEChMKR2xvYmFsU2lnbjETMBEGA1UEAxMKR2xvYmFsU2lnbjCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAKbPJA6+Lm8omUVCxKs+IVSbC9N/hHD6ErPLv4dfxn+G07IwXNb9rfF73OX4YJYJkhD10FPe+3t+c4isUoh7SqbKSaZeqKeMWhG8eoLrvozps6yWJQeXSpkqBy+0Hne/ig+1AnwblrjFuTosvNYSuetZfeLQBoZfXklqtTleiDTsvHgMCJiEbKjNS7SgfQx5TfC4LcshytVsW33hoCmEofnTlEnLJGKRILzdC9XZzPnqJworc5HGnRusyMvo4KD0L5CLTfuwNhv2GXqF4G3yYROIXJ/gkwpRl4pazq+r1feqCapgvdzZX99yqWATXgAByUr6P6TqBwMhAo6CygPCm48CAwEAAaOBnDCBmTAOBgNVHQ8BAf8EBAMCAQYwDwYDVR0TAQH/BAUwAwEB/zAdBgNVHQ4EFgQUm+IHV2ccHsBqBt5ZtJot39wZhi4wNgYDVR0fBC8wLTAroCmgJ4YlaHR0cDovL2NybC5nbG9iYWxzaWduLm5ldC9yb290LXIyLmNybDAfBgNVHSMEGDAWgBSb4gdXZxwewGoG3lm0mi3f3BmGLjANBgkqhkiG9w0BAQUFAAOCAQEAmYFThxxol4aR7OBKuEQLq4GsJ0/WwbgcQ3izDJr86iw8bmEbTUsp9Z8FHSbBuOmDAGJFtqkIk7mpM0sYmsL4h4hO291xNBrBVNpGP+DTKqttVCL1OmLNIG+6KYnX3ZHu01yiPqFbQfXf5WRDLenVOavSot+3i9DAgBkcRcAtjOj4LaR0VknFBbVPFd5uRHg5h6h+u/N5GJG79G+dwfCMNYxdAfvDbbnvRG15RjF+Cv6pgsH/76tuIMRQyV+dTZsXjAzlAcmgQWpzU/qlULRuJQ/7TBj0/VLZjmmx6BEP3ojY+x1J96relc8geMJgEtslQIxq/H5COEBkEveegeGTLg=="
                }
            };

            using var httpClientHandler = new HttpClientHandler();
            if (!string.IsNullOrWhiteSpace(appConfig.Proxy))
            {
                Console.WriteLine("Using proxy: " + appConfig.Proxy);
                httpClientHandler.Proxy = new WebProxy(new Uri(appConfig.Proxy));
            }
            if (appConfig.Certs.Any())
            {
                var trustedCertRawDatas = appConfig.Certs.Select(Convert.FromBase64String).ToList();

                httpClientHandler.ServerCertificateCustomValidationCallback = (httpRequestMessage, x509Certificate2, x509Chain, sslPolicyErrors) => {
                    // See https://www.meziantou.net/custom-certificate-validation-in-dotnet.htm
                    foreach (var element in x509Chain.ChainElements)
                    {
                        var cert = element.Certificate;
                        Console.WriteLine("Inspecting cert: " + cert.Subject);
                        var errors = element.ChainElementStatus;
                        if (errors.Length > 0)
                        {
                            Console.WriteLine("Statuses: " + string.Join(", ", errors.Select(x => x.Status)));
                            if (!trustedCertRawDatas.Any(rawData => rawData.SequenceEqual(cert.RawData)))
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                };
            }

            using var httpClient = new HttpClient(httpClientHandler);

            await Console.Out.WriteLineAsync("GET " + appConfig.RemoteUri);

            var response = await httpClient.SendAsync(new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(appConfig.RemoteUri)
            });


            await Console.Out.WriteLineAsync(response.ToString());
        }
    }
}

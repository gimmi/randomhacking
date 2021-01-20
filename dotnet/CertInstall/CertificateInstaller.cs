using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Extensions.Logging;

namespace CertInstall
{
    public class CertificateInstaller
    {
        private readonly ILogger<CertificateInstaller> _logger;

        public CertificateInstaller(ILogger<CertificateInstaller> logger) => _logger = logger;

        // Inspired by
        // https://github.com/Azure/iotedge/blob/50f43dd/samples/dotnet/EdgeDownstreamDevice/Program.cs#L74
        public void InstallRootCas(IEnumerable<string> trustedRootCas)
        {
            _logger.LogDebug("Attempting to open Root store");
            using var x509Store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            x509Store.Open(OpenFlags.ReadWrite);
            foreach (var x509Certificate in ParseCerts(trustedRootCas))
            {
                _logger.LogDebug("Installing {}", x509Certificate.Subject);
                x509Store.Add(new X509Certificate2(x509Certificate));
            }
        }

        public static IEnumerable<X509Certificate> ParseCerts(IEnumerable<string> pemLines)
        {
            using var en = pemLines.GetEnumerator();
            while (en.MoveNext())
            {
                if ("-----BEGIN CERTIFICATE-----".Equals(en.Current))
                {
                    var sb = new StringBuilder();
                    while (en.MoveNext())
                    {
                        if ("-----END CERTIFICATE-----".Equals(en.Current))
                        {
                            break;
                        }

                        sb.Append(en.Current);
                    }

                    yield return new X509Certificate(Convert.FromBase64String(sb.ToString()));
                }
            }
        }
    }
}
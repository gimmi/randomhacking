using System;
using Nancy.Hosting.Self;

namespace SpikeNancyFx
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var baseUri = new Uri("http://localhost:8080");
            var hostConfiguration = new HostConfiguration {
                UrlReservations = new UrlReservations {
                    CreateAutomatically = true
                }
            };
            using (var host = new NancyHost(hostConfiguration, baseUri))
            {
                host.Start();
                Console.WriteLine($"Running on {baseUri}");
                Console.ReadLine();
            }
        }
    }
}
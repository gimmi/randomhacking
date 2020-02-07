using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SpikeGrpc
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureLogging(logging => {
                    logging.SetMinimumLevel(LogLevel.Trace);
                    logging.AddConsole();
                })
                .UseDefaultServiceProvider(provider => {
                    provider.ValidateScopes = true;
                    provider.ValidateOnBuild = true;
                })
                .ConfigureServices(services => {
                    services.AddGrpc();
                })
                .ConfigureWebHostDefaults(web => {
                    web.UseUrls("http://0.0.0.0:5052");
                    web.ConfigureKestrel(kestrel => {
                        kestrel.ConfigureEndpointDefaults(endpoints => {
                            endpoints.Protocols = HttpProtocols.Http2;
                        });
                    });

                    web.Configure(app => {
                        app.UseRouting();
                        app.UseEndpoints(endpoints => {
                            endpoints.MapGrpcService<GreeterService>();
                        });
                    });
                })
                .UseConsoleLifetime(console => {
                    console.SuppressStatusMessages = true;
                })
                .Build();

            await host.RunAsync();
        }
    }
}

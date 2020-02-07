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
        public static void Main(string[] args)
        {
            var host = new HostBuilder()
                .UseEnvironment(Environments.Development)
                .ConfigureLogging(logging => {
                    logging.SetMinimumLevel(LogLevel.Trace);
                    logging.AddConsole();
                })
                .UseDefaultServiceProvider((context, options) => {
                    var isDevelopment = context.HostingEnvironment.IsDevelopment();
                    options.ValidateScopes = isDevelopment;
                    options.ValidateOnBuild = isDevelopment;
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
                .Build();

            host.Run();
        }
    }
}

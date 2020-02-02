using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
                .ConfigureWebHostDefaults(webHost => {
                    webHost.ConfigureKestrel(kestrel => {
                        kestrel.Listen(IPAddress.Any, 5052, listen => {
                            listen.Protocols = HttpProtocols.Http2;
                        });
                    });

                    webHost.Configure((env, app) => {
                        if (env.HostingEnvironment.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }

                        app.UseRouting();

                        app.UseEndpoints(endpoints => {
                            endpoints.MapGrpcService<GreeterService>();

                            endpoints.MapGet("/", async context => {
                                await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                            });
                        });

                    });
                })
                .Build();

            host.Run();
        }
    }
}

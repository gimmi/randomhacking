using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

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
                .ConfigureWebHostDefaults(webHost => {
                    webHost.ConfigureKestrel(kestrel => {
                        kestrel.Listen(IPAddress.Any, 5052, listen => {
                            listen.Protocols = HttpProtocols.Http2;
                        });
                    });
                    webHost.UseStartup<Startup>();
                })
                .Build();

            host.Run();
        }
    }
}

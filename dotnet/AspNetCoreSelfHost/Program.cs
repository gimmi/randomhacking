using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpikeMicrosoftExtensions;

namespace AspNetCoreSelfHost
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var webHost = new WebHostBuilder()
                .UseKestrel(options => {
                    options.AddServerHeader = false;
                })
                .UseEnvironment(EnvironmentName.Development)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseDefaultServiceProvider(options => {
                    options.ValidateScopes = true;
                })
                .ConfigureServices(services => {
                    services.AddScoped<IFooService, FooService>();
                    services.AddMvc();
                })
                .ConfigureLogging(logging => {
                    logging.SetMinimumLevel(LogLevel.Warning);
                    logging.AddFilter("AspNetCoreSelfHost", LogLevel.Trace);
                    logging.AddConsole();
                })
                .Configure(app => {
                    var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();
                    if (env.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();
                    }

                    app.UseMvc();
                })
                .Build();

            webHost.Run();
        }
    }
}

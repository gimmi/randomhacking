using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SpikeMicrosoftExtensions;

namespace AspNetCoreSelfHost
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder().Build();

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder()
        {
            return new WebHostBuilder()
                .UseKestrel(options => {
                    options.AddServerHeader = false;
                })
                .UseEnvironment(EnvironmentName.Development)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseDefaultServiceProvider(options => {
                    options.ValidateScopes = true;
                })
                .ConfigureServices(services => {
                    services.AddMvc()
                        .AddJsonOptions(json => {
                            json.SerializerSettings.Formatting = Formatting.Indented;
                            json.SerializerSettings.ContractResolver = new DefaultContractResolver {
                                NamingStrategy = new CamelCaseNamingStrategy {
                                    ProcessDictionaryKeys = false
                                }
                            };
                        });
                    services.AddScoped<IFooService, FooService>();
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
                });
        }
    }
}

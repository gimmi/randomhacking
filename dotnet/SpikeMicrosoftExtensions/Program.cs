using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SpikeMicrosoftExtensions
{
    public class Program
    {
        public static void Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    // See https://msdn.microsoft.com/en-us/magazine/mt694089.aspx

                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddConsole();
                })
                .AddSingleton<IFooService, FooService>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Starting...");

            var fooService = serviceProvider.GetRequiredService<IFooService>();

            fooService.Serve();

            serviceProvider.Dispose();
        }
    }
}
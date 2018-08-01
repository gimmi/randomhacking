﻿using System;
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
                .AddScoped<IFooService, FooService>()
                .AddSingleton<IPlugin, Plugin1>()
                .AddSingleton<IPlugin, Plugin2>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Starting");

//            TestLoggingConfiguration(serviceProvider);

//            TestPluginSystem(serviceProvider);

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<IFooService>();
            }
            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<IFooService>();
            }
            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<IFooService>();
            }

            logger.LogInformation("Stopping");
            serviceProvider.Dispose();
        }

        private static void TestPluginSystem(ServiceProvider serviceProvider)
        {
            foreach (var plugin in serviceProvider.GetServices<IPlugin>())
            {
                plugin.Action();
            }
        }

        private static void TestLoggingConfiguration(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Starting {useless} - {useless} - {useless} - {useless,6} - {useless,-6} - {useless:o}", "a", new[] {1, 2, 3}, null, 3.14, 3.14, DateTime.Now);

            var fooService = serviceProvider.GetRequiredService<IFooService>();
            fooService.LogSomething();
        }
    }
}
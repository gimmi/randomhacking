using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace SpikeNLog
{
    class Program
    {
        static void Main(string[] args)
        {
            NLogConfig.Activate();

            var serviceProvider = BuildDi();

            //var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            //loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            //NLog.LogManager.LoadConfiguration("nlog.config");

            var runner = serviceProvider.GetRequiredService<Runner>();
            runner.Run();

            Console.WriteLine("Hello World!");

            Console.ReadKey();
        }

        private static ServiceProvider BuildDi()
        {
            return new ServiceCollection()
                .AddLogging(builder => {
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddNLog(new NLogProviderOptions {
                        CaptureMessageTemplates = true,
                        CaptureMessageProperties = true
                    });
                })
                .AddTransient<Runner>()
                .BuildServiceProvider();
        }
    }

    public class Runner
    {
        private readonly ILogger<Runner> _logger;

        public Runner(ILogger<Runner> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogTrace("This is a Trace level log entry");
            _logger.LogDebug("This is a Debug level log entry");
            _logger.LogInformation("This is a Information level log entry");
            _logger.LogWarning("This is a Warning level log entry");
            _logger.LogError("This is a Error level log entry");
            _logger.LogCritical("This is a Critical level log entry");
        }
    }
}

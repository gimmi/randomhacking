using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;

namespace SpikeSerilog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var jsonLogging = true;

            var serviceProvider = new ServiceCollection()
                .AddLogging(builder => {
                    builder.SetMinimumLevel(LogLevel.Trace);

                    var serilogCfg = new LoggerConfiguration();
                    if (jsonLogging)
                    {
                        serilogCfg.WriteTo.Console(new JsonFormatter(renderMessage: true));
                    }
                    else
                    {
                        serilogCfg.WriteTo.Console(
                            outputTemplate: "{Level:u3} {SourceContext} {Message:lj}{NewLine}{Exception}",
                            theme: ConsoleTheme.None
                        );
                    }

                    builder.AddSerilog(serilogCfg.CreateLogger(), true);
                })
                .BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Starting");

            logger.LogTrace("Message with par1={par1} and par2={}", 123, 456);
            logger.LogDebug("Message with par1={par1} and par2={}", 123, 456);
            logger.LogInformation("Message with par1={par1} and par2={}", 123, 456);
            logger.LogWarning("Message with par1={par1} and par2={}", 123, 456);
            logger.LogError("Message with par1={par1} and par2={}", 123, 456);
            logger.LogCritical("Message with par1={par1} and par2={}", 123, 456);

            try
            {
                throw new ApplicationException("AHHH");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Exception caught!");
            }


            logger.LogInformation("Stopping");
            serviceProvider.Dispose();
        }
    }
}

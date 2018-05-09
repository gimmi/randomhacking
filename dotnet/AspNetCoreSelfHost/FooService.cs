using System;
using Microsoft.Extensions.Logging;
using SpikeMicrosoftExtensions;

namespace AspNetCoreSelfHost
{
    public class FooService : IFooService, IDisposable
    {
        private readonly ILogger<FooService> _logger;

        public FooService(ILogger<FooService> logger)
        {
            _logger = logger;

            _logger.LogInformation(".ctor");
        }

        public void LogSomething()
        {
            _logger.LogTrace("This is a Trace level log entry");
            _logger.LogDebug("This is a Debug level log entry");
            _logger.LogInformation("This is a Information level log entry");
            _logger.LogWarning("This is a Warning level log entry");
            _logger.LogError("This is a Error level log entry");
            _logger.LogCritical("This is a Critical level log entry");
        }

        public void Dispose()
        {
            _logger.LogInformation("disposing");
        }
    }
}
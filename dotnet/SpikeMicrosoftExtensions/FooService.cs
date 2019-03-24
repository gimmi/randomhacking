using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace SpikeMicrosoftExtensions
{
    public class FooService : IFooService, IDisposable
    {
        private readonly ILogger<FooService> _logger;
        private readonly DiagnosticListener _diagnosticListener;

        public FooService(ILogger<FooService> logger, DiagnosticListener diagnosticListener)
        {
            _logger = logger;

            _logger.LogInformation(".ctor");
            _diagnosticListener = diagnosticListener;
        }

        public void LogSomething()
        {
            if (_diagnosticListener.IsEnabled("IncomingCall"))
            {
                _diagnosticListener.Write("IncomingCall", 123);
            }
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
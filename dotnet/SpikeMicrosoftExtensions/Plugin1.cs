using System;
using Microsoft.Extensions.Logging;

namespace SpikeMicrosoftExtensions
{
    public class Plugin1 : IPlugin, IDisposable
    {
        private readonly ILogger _logger;

        public Plugin1(ILogger<Plugin1> logger)
        {
            _logger = logger;
            _logger.LogInformation(".ctor");
        }

        public void Action()
        {
            _logger.LogInformation("Running plugin 1");
        }

        public void Dispose()
        {
            _logger.LogInformation("disposing");
        }
    }
}
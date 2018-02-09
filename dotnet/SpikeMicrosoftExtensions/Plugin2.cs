using System;
using Microsoft.Extensions.Logging;

namespace SpikeMicrosoftExtensions
{
    public class Plugin2 : IPlugin, IDisposable
    {
        private readonly ILogger _logger;

        public Plugin2(ILogger<Plugin2> logger)
        {
            _logger = logger;
            _logger.LogInformation(".ctor");
        }

        public void Action()
        {
            _logger.LogInformation("Running plugin 2");
        }

        public void Dispose()
        {
            _logger.LogInformation("disposing");
        }
    }
}
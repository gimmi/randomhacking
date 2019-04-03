using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace SpikeMicrosoftExtensions
{
    public class PluginManager
    {
        private readonly ILogger<PluginManager> _logger;
        private readonly IEnumerable<IPlugin> _plugins;

        public PluginManager(ILogger<PluginManager> logger, IEnumerable<IPlugin> plugins)
        {
            _logger = logger;
            _plugins = plugins;
        }

        public void Action()
        {
            foreach (var plugin in _plugins)
            {
                plugin.Action();
            }
        }
    }
}
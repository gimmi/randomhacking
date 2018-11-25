using NLog;
using NLog.Config;
using NLog.Targets;
using System;

namespace SpikeNLog
{
    public static class NLogConfig
    {
        public static void Activate()
        {
            var consoleTarget = new ColoredConsoleTarget("console") {
                Layout = "${logger} ${message}"
            };

            // See https://github.com/NLog/NLog/wiki/NLogViewer-target
            var viewerTarget = new NLogViewerTarget("log4view") {
                Address = "udp4://127.0.0.1:878"
            };

            var cfg = new LoggingConfiguration();
            cfg.AddTarget(consoleTarget);
            cfg.AddRuleForAllLevels(consoleTarget);

            cfg.AddTarget(viewerTarget);
            cfg.AddRuleForAllLevels(viewerTarget);

            LogManager.Configuration = cfg;
        }
    }
}

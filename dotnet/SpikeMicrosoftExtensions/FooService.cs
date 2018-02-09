using Microsoft.Extensions.Logging;

namespace SpikeMicrosoftExtensions
{
    public class FooService : IFooService
    {
        private readonly ILogger<FooService> _logger;

        public FooService(ILogger<FooService> logger)
        {
            _logger = logger;
        }

        public void Serve()
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
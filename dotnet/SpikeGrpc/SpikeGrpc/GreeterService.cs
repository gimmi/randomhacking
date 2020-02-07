using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace SpikeGrpc
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloResponse> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogInformation("SayHello({})", request.Name);
            return Task.FromResult(new HelloResponse {
                Message = "Hello " + request.Name
            });
        }
    }
}

using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SpikeGrpc
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public GreeterService(ILogger<GreeterService> logger, IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;
            _applicationLifetime = applicationLifetime;
        }

        public override Task<HelloResponse> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogInformation("SayHello({})", request.Name);
            return Task.FromResult(new HelloResponse {
                Message = "Hello " + request.Name
            });
        }

        public override async Task Echo(IAsyncStreamReader<EchoRequest> requestStream, IServerStreamWriter<EchoResponse> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext(_applicationLifetime.ApplicationStopping))
            {
                var message = requestStream.Current.Message;
                _logger.LogInformation("=> {}", message);
                await responseStream.WriteAsync(new EchoResponse {Message = message});
            }
        }
    }
}

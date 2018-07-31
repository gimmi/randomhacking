using System.Threading.Tasks;
using Grpc.Core;
using Spikegrpc.Proto;

namespace SpikeGrpc
{
    public class EchoServiceServer : EchoService.EchoServiceBase
    {
        public override Task<EchoResponse> Echo(EchoRequest request, ServerCallContext context)
        {
            return Task.FromResult(new EchoResponse {Message = request.Message});
        }
    }
}
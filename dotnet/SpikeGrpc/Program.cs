using System;
using System.Threading.Tasks;
using Grpc.Core;
using Spikegrpc.Proto;

namespace SpikeGrpc
{
    public static class Program
    {
        public static async Task Main()
        {
            const int port = 50052;
            var server = new Server {
                Services = { EchoService.BindService(new EchoServiceServer()) },
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
            };
            server.Start();
            
            await Console.Out.WriteLineAsync("RouteGuide server listening on port " + port);

            var channel = new Channel("localhost", port, ChannelCredentials.Insecure);
            var client = new EchoService.EchoServiceClient(channel);

            var echoRequest = new EchoRequest {Message = "Hello GRPC!"};
            var echoResponse = await client.EchoAsync(echoRequest);
            
            await Console.Out.WriteLineAsync($"EchoService.Echo({echoRequest.Message}) => {echoResponse.Message}");

            await Console.Out.WriteLineAsync("Press enter to complete...");
            
            await Console.In.ReadLineAsync();

            await channel.ShutdownAsync();
            await server.ShutdownAsync();
            
            await Console.Out.WriteLineAsync("Done.");
        }
    }
}

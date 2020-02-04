using System;
using System.Threading.Tasks;
using Cocona;
using Grpc.Net.Client;

namespace SpikeGrpc.Cli
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            await CoconaApp.RunAsync<Program>(args);
        }

        public async Task Main()
        {
            var channel = GrpcChannel.ForAddress("http://127.0.0.1:5052");
            var client = new Greeter.GreeterClient(channel);

            var req = new HelloRequest{ Name = "Gimmi" };
            var resp = await client.SayHelloAsync(req);

            await Console.Out.WriteLineAsync(resp.Message);
        }
    }
}

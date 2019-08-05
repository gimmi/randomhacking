using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTPServer
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var ct = BindCtrlC();
            var theServer = new TheServer();
            await theServer.ConnectAsync(22);

            try
            {
                await Task.Delay(Timeout.Infinite, ct);
            }
            catch (OperationCanceledException)
            {
            }

            await theServer.DisconnectAsync();
        }

        private static async Task ClientLoop(Task<TcpClient> tcpClientTask, CancellationToken ct)
        {
            using (var tcpClient = await tcpClientTask)
            {
                var stream = tcpClient.GetStream();
                var streamReader = new StreamReader(stream, Encoding.ASCII);
                while (!streamReader.EndOfStream)
                {
                    var line = await streamReader.ReadLineAsync();
                    await Console.Out.WriteLineAsync(line);
                }
            }
            await Console.Out.WriteLineAsync("Disconnected");
        }

        public static CancellationToken BindCtrlC()
        {
            var stopCts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) => {
                e.Cancel = true;
                stopCts.Cancel();
            };
            return stopCts.Token;
        }
    }
}

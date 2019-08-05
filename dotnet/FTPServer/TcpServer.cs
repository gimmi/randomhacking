using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTPServer
{
    public class TcpServer
    {
        private CancellationTokenSource _stopCts;
        private TcpListener _tcpListener;
        private Task _acceptTask;

        public async Task ConnectAsync(int port)
        {
            await Console.Out.WriteLineAsync("Listening on port " + port);
            _stopCts = new CancellationTokenSource();
            _tcpListener = TcpListener.Create(port);
            _tcpListener.Start();
            _acceptTask = Task.Run(AcceptLoop);
        }

        public async Task DisconnectAsync()
        {
            await Console.Out.WriteLineAsync("Stopping");
            _stopCts.Cancel();
            _tcpListener.Stop();
            await _acceptTask;
        }

        private async Task AcceptLoop()
        {
            while (true)
            {
                var tcpClient = await _tcpListener.AcceptTcpClientAsync()
                    .DefaultIfCanceledAsync(_stopCts.Token);
                if (tcpClient == default)
                {
                    break;
                }

                // TODO what to do with this??
                var clientTask = Task.Run(() => ClientLoop(tcpClient), _stopCts.Token);
            }
        }

        private async Task ClientLoop(TcpClient tcpClient)
        {
            await Console.Out.WriteLineAsync("Connected");
            try
            {
                var stream = tcpClient.GetStream();
                var streamReader = new StreamReader(stream, Encoding.ASCII);

                while (true)
                {
                    var line = await streamReader.ReadLineAsync()
                        .DefaultIfCanceledAsync(_stopCts.Token);

                    if (line == default)
                    {
                        break;
                    }
                    await Console.Out.WriteLineAsync(line);
                }
            }
            finally
            {
                tcpClient.Dispose();
            }
            await Console.Out.WriteLineAsync("Disconnected");
        }
    }
}

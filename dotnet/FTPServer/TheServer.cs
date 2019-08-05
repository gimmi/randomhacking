using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTPServer
{
    public class TheServer
    {
        private CancellationTokenSource _stopCts;
        private TcpListener _tcpListener;
        private Task _connectTask;

        public async Task ConnectAsync(int port)
        {
            await Console.Out.WriteLineAsync("Listening on port " + port);
            _stopCts = new CancellationTokenSource();
            _tcpListener = TcpListener.Create(port);
            _tcpListener.Start();
            _connectTask = Task.Run(ConnectLoop);
        }

        public async Task DisconnectAsync()
        {
            await Console.Out.WriteLineAsync("Stopping");
            _stopCts.Cancel();
            _tcpListener.Stop();
            await _connectTask;
        }

        private async Task ConnectLoop()
        {
            while (!_stopCts.IsCancellationRequested)
            {
                var tcpClientTask = _tcpListener.AcceptTcpClientAsync();
                var cancelTask = Task.Delay(Timeout.Infinite, _stopCts.Token);
                await Task.WhenAny(tcpClientTask, cancelTask);
                if (_stopCts.IsCancellationRequested)
                {
                    break;
                }

                // TODO what to do with this??
                var clientTask = Task.Run(() => ClientLoop(tcpClientTask), _stopCts.Token);
            }
        }

        private async Task ClientLoop(Task<TcpClient> tcpClientTask)
        {
            using (var tcpClient = await tcpClientTask)
            {
                var stream = tcpClient.GetStream();
                var streamReader = new StreamReader(stream, Encoding.ASCII);

                while (!_stopCts.IsCancellationRequested)
                {
                    var lineTask = streamReader.ReadLineAsync();
                    var cancelTask = Task.Delay(Timeout.Infinite, _stopCts.Token);
                    await Task.WhenAny(tcpClientTask, cancelTask);
                    if (_stopCts.IsCancellationRequested)
                    {
                        break; // Server is shutting down
                    }
                    var line = await lineTask;
                    if (line == null)
                    {
                        break; // Client is shutting down
                    }

                    await Console.Out.WriteLineAsync(line);
                }
            }
            await Console.Out.WriteLineAsync("Disconnected");
        }
    }
}
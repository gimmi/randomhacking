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

                var clientTask = Task.Run(new FtpControlConnection(tcpClient, _stopCts.Token).LoopAsync, _stopCts.Token);
            }
        }
    }

    public class FtpControlConnection
    {
        private readonly TcpClient _tcpClient;
        private readonly CancellationToken _stopCt;

        public FtpControlConnection(TcpClient tcpClient, CancellationToken stopCt)
        {
            _tcpClient = tcpClient;
            _stopCt = stopCt;
        }

        public async Task LoopAsync()
        {
            await Console.Out.WriteLineAsync("Connected");
            try
            {
                var stream = _tcpClient.GetStream();
                var streamReader = new StreamReader(stream, Encoding.ASCII);
                var streamWriter = new StreamWriter(stream, Encoding.ASCII);

                // See https://www.ncftp.com/libncftp/doc/ftp_overview.html
                await streamWriter.WriteLineAsync("220 Service ready for new user");
                await streamWriter.FlushAsync();

                while (true)
                {
                    var cmd = await streamReader.ReadLineAsync()
                        .DefaultIfCanceledAsync(_stopCt);

                    if (cmd == default)
                    {
                        break;
                    }

                    await Console.Out.WriteLineAsync("=> " + cmd);

                    if (cmd.StartsWith("USER "))
                    {
                        await streamWriter.WriteLineAsync("230 User logged in");
                        await streamWriter.FlushAsync();
                    }
                    else if (cmd == "QUIT")
                    {
                        await streamWriter.WriteLineAsync("221 Service closing control connection");
                        await streamWriter.FlushAsync();
                        break;
                    }
                    else if (cmd == "PASV")
                    {
                        await streamWriter.WriteLineAsync("227 Entering Passive Mode (h1,h2,h3,h4,p1,p2)");
                        await streamWriter.FlushAsync();
                        break;
                    }
                    else
                    {
                        await streamWriter.WriteLineAsync("202 Command not implemented");
                        await streamWriter.FlushAsync();
                    }
                }
            }
            finally
            {
                _tcpClient.Dispose();
            }
            await Console.Out.WriteLineAsync("Disconnected");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTPServer
{
    public class FtpServer
    {
        private CancellationTokenSource _stopCts;
        private TcpListener _tcpListener;
        private Task _acceptTask;
        private IPAddress _ipAddress;
        private List<ClientData> _clients;

        public async Task ConnectAsync(IPAddress ipAddress, int controlPort, int dataPortBase, int dataportCount)
        {
            await Console.Out.WriteLineAsync($"Listening on {ipAddress}:{controlPort}");
            _clients = Enumerable.Range(dataPortBase, dataportCount)
                .Select(i => new ClientData {Port = i, Task = Task.CompletedTask})
                .ToList();
            _ipAddress = ipAddress;
            _stopCts = new CancellationTokenSource();
            _tcpListener = new TcpListener(ipAddress, controlPort);
            _tcpListener.Start();
            _acceptTask = Task.Run(AcceptLoop);
        }

        public async Task DisconnectAsync()
        {
            await Console.Out.WriteLineAsync("Stopping");
            _tcpListener.Stop();
            _stopCts.Cancel();
            await _acceptTask;
            foreach (var client in _clients)
            {
                await client.Task;
            }
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

                var client = _clients.FirstOrDefault(x => x.Task.IsCompleted);
                if (client == null)
                {
                    var stream = tcpClient.GetStream();
                    var writer = new StreamWriter(stream, Encoding.ASCII);
                    await writer.WriteLineAsync("421 Too Many Connections");
                    await writer.FlushAsync();
                    tcpClient.Dispose();
                }
                else
                {
                    await client.Task;
                    var ftpClientHandler = new FtpClientHandler(tcpClient, _stopCts.Token, new IPEndPoint(_ipAddress, client.Port));
                    client.Task = Task.Run(ftpClientHandler.HandleAsync, _stopCts.Token);
                }
            }
        }

        private class ClientData
        {
            public int Port { get; set; }
            public Task Task { get; set; }
        }

        public class FtpClientHandler
        {
            private readonly TcpClient _tcpClient;
            private readonly CancellationToken _stopCt;
            private readonly IPEndPoint _dataIpEndPoint;

            public FtpClientHandler(TcpClient tcpClient, CancellationToken stopCt, IPEndPoint dataIpEndPoint)
            {
                _tcpClient = tcpClient;
                _stopCt = stopCt;
                _dataIpEndPoint = dataIpEndPoint;
            }

            public async Task HandleAsync()
            {
                await Console.Out.WriteLineAsync("Connected");
                try
                {
                    var stream = _tcpClient.GetStream();
                    var reader = new StreamReader(stream, Encoding.ASCII);
                    var writer = new StreamWriter(stream, Encoding.ASCII);

                    // See https://www.ncftp.com/libncftp/doc/ftp_overview.html
                    await writer.WriteLineAsync("220 Service ready for new user");
                    await writer.FlushAsync();

                    var dataTcpListener = new TcpListener(_dataIpEndPoint);
                    dataTcpListener.Start();

                    while (true)
                    {
                        var cmd = await reader.ReadLineAsync()
                            .DefaultIfCanceledAsync(_stopCt);

                        if (cmd == default)
                        {
                            break;
                        }

                        await Console.Out.WriteLineAsync("=> " + cmd);

                        if (cmd == "QUIT")
                        {
                            await writer.WriteLineAsync("221 Service closing control connection");
                            await writer.FlushAsync();
                            break;
                        }

                        if (cmd.StartsWith("USER "))
                        {
                            await writer.WriteLineAsync("230 User logged in");
                            await writer.FlushAsync();
                        }
                        else if (cmd == "PASV")
                        {
                            var addr = new StringBuilder("(")
                                .AppendJoin(',', _dataIpEndPoint.Address.GetAddressBytes())
                                .Append(',').Append(_dataIpEndPoint.Port >> 8)
                                .Append(',').Append(_dataIpEndPoint.Port & 0xFF)
                                .Append(")")
                                .ToString();
                            await writer.WriteLineAsync("227 Entering Passive Mode " + addr);
                            await writer.FlushAsync();
                        }
                        else if (cmd == "NOOP")
                        {
                            await writer.WriteLineAsync("200 OK");
                            await writer.FlushAsync();
                        }
                        else if (cmd == "TYPE I")
                        {
                            await writer.WriteLineAsync("200 Type set to BINARY");
                            await writer.FlushAsync();
                        }
                        else if (cmd.StartsWith("STOR "))
                        {
                            var filePath = cmd.Substring(5);
                            await writer.WriteLineAsync("150 About to open data connection");
                            await writer.FlushAsync();

                            using (var tcpClient = await dataTcpListener.AcceptTcpClientAsync().DefaultIfCanceledAsync(_stopCt))
                            {
                                if (tcpClient == default)
                                {
                                    break;
                                }

                                var ms = new MemoryStream();
                                await tcpClient.GetStream().CopyToAsync(ms);
                                var fileContent = ms.ToArray();

                                // TODO do something with data
                                await Console.Out.WriteLineAsync($"{filePath}: {Encoding.ASCII.GetString(fileContent)}");
                            }

                            await writer.WriteLineAsync("226 Transfer complete");
                            await writer.FlushAsync();
                        }
                        else
                        {
                            await writer.WriteLineAsync("202 Command not implemented");
                            await writer.FlushAsync();
                        }
                    }

                    dataTcpListener.Stop();
                }
                finally
                {
                    _tcpClient.Dispose();
                }

                await Console.Out.WriteLineAsync("Disconnected");
            }
        }
    }
}

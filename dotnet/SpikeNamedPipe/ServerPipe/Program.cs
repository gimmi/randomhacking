using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace ServerPipe
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await OutAsync("Starting");

            var ctrlC = BindCtrlC();

            while (!ctrlC.IsCancellationRequested)
            {
                try
                {
                    await OutAsync("Listening for connection");
                    await using var pipeServer = new NamedPipeServerStream("testpipe", PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                    await pipeServer.WaitForConnectionAsync(ctrlC);

                    while (!ctrlC.IsCancellationRequested)
                    {
                        var buffer = new byte[1_024];

                        await OutAsync("Waiting for data");
                        var length = await pipeServer.ReadAsync(buffer, 0, buffer.Length, ctrlC);

                        await OutAsync("Received: " + BitConverter.ToString(buffer, 0, length));
                    }
                    pipeServer.Disconnect();
                }
                catch (TaskCanceledException e) when (e.CancellationToken == ctrlC)
                {
                    await OutAsync("Ctrl-C");
                }
                catch (Exception e)
                {
                    await OutAsync(e);
                }
            }

            await OutAsync("Stopping server");
        }

        public static Task OutAsync(object msg) => Console.Out.WriteLineAsync("SRV - " + msg);

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

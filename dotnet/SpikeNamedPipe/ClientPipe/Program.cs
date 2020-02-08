using System;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace ClientPipe
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await OutAsync("Starting client");

            var ctrlC = BindCtrlC();

            while (!ctrlC.IsCancellationRequested)
            {
                try
                {
                    await OutAsync("Connecting");

                    await using var pipeClient = new NamedPipeClientStream(".", "testpipe", PipeDirection.InOut, PipeOptions.Asynchronous);
                    await pipeClient.ConnectAsync(ctrlC);

                    var value = 0;

                    while (!ctrlC.IsCancellationRequested)
                    {
                        var buffer = BitConverter.GetBytes(++value);
                        await pipeClient.WriteAsync(buffer, 0, buffer.Length, ctrlC);

                        await OutAsync("Write: " + buffer.Length);

                        await Task.Delay(1_000, ctrlC);
                    }
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
        }

        public static Task OutAsync(object msg) => Console.Out.WriteLineAsync("CLI - " + msg);

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

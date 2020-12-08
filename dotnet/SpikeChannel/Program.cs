using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SpikeChannel
{
    public class Program
    {
        private static int _writeCount;
        private static int _lostCount;
        private static int _readCount;

        public static async Task Main()
        {
            var channel = Channel.CreateUnbounded<string>(new UnboundedChannelOptions { SingleReader = true });

            var (writer, reader) = (channel.Writer, channel.Reader);

            var ctrlC = BindCtrlC();

            await Task.WhenAll(
                WriteAsync(writer, "Prod1", ctrlC),
                WriteAsync(writer, "Prod2", ctrlC),
                ReaderAsync(reader, ctrlC)
            );
            
            await Console.Out.WriteLineAsync($"Done, {_writeCount} writes, {_lostCount} lost, {_readCount} read");
        }

        private static CancellationToken BindCtrlC()
        {
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) => {
                e.Cancel = true;
                cts.Cancel();
            };
            return cts.Token;
        }

        private static async Task WriteAsync(ChannelWriter<string> writer, string prefix, CancellationToken ct)
        {
            var counter = 0;
            while (await WaitAsync(ct))
            {
                var message = $"{prefix}: {++counter}";
                if (writer.TryWrite(message))
                {
                    Interlocked.Increment(ref _writeCount);
                }
                else
                {
                    Interlocked.Increment(ref _lostCount);
                }
            }
        }

        private static async Task ReaderAsync(ChannelReader<string> reader, CancellationToken ct)
        {
            try
            {
                while (true)
                {
                    var message = await reader.ReadAsync(ct);
                    Interlocked.Increment(ref _readCount);
                    await Console.Out.WriteLineAsync(message);
                }
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                // Cancellation requested by caller
            }
        }

        private static async Task<bool> WaitAsync(CancellationToken ct)
        {
            try
            {
                await Task.Delay(1, ct);
                return true;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
        }
    }
}

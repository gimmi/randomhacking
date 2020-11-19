using System;
using System.Data;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SpikeChannel
{
    class Program
    {
        private static int LossCount = 0;
        
        static async Task Main(string[] args)
        {
            var channel = Channel.CreateUnbounded<string>(new UnboundedChannelOptions {
                SingleWriter = false,
                SingleReader = true,
                AllowSynchronousContinuations = true
            });

            var (writer, reader) = (channel.Writer, channel.Reader);

            var ct = BindCtrlC();

            await Task.WhenAll(
                WriteAsync(writer, "Prod1", ct),
                WriteAsync(writer, "Prod2", ct),
                ReaderAsync(reader, ct)
            );
            
            Console.WriteLine("Done");
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
            var counter = 1;
            while (!ct.IsCancellationRequested)
            {
                if (!writer.TryWrite($"{prefix}: {counter}"))
                {
                    LossCount++;
                }
                counter++;
                await Task.Delay(100, ct);
            }
        }

        private static async Task ReaderAsync(ChannelReader<string> reader, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                var message = await reader.ReadAsync(ct);
                await Console.Out.WriteLineAsync($"{LossCount} | {message}");
            }
        }
    }
}

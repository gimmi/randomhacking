using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SpikeDotnetCounters
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var ct = BindCtrlC();
            while (await DelayAsync(ct))
            {
                SampleEventSource.Instance.NotifyReceivedLog();
                await Console.Out.WriteLineAsync("XXX");
                SampleEventSource.Instance.NotifySentLog();
            }
        }

        private static async Task<bool> DelayAsync(CancellationToken ct)
        {
            try
            {
                await Task.Delay(500, ct);
                return true;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
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

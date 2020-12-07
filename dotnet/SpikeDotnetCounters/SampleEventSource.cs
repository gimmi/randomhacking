using System;
using System.Diagnostics.Tracing;
using System.Threading;

namespace SpikeDotnetCounters
{
    [EventSource(Name = "SpikeDotnetCounters")]
    internal sealed class SampleEventSource : EventSource
    {
        public static readonly SampleEventSource Instance = new SampleEventSource();

        private IncrementingEventCounter? _receivedLogsPc;
        private IncrementingEventCounter? _sentLogsPc;
        private IncrementingEventCounter? _discardedLogsPc;

        protected override void OnEventCommand(EventCommandEventArgs args)
        {
            if (args.Command == EventCommand.Enable)
            {
                _receivedLogsPc ??= new IncrementingEventCounter("receivedLogs", this) { DisplayName = "Incoming logs",  DisplayUnits = "logs" };
                _sentLogsPc ??= new IncrementingEventCounter("sentLogs", this) { DisplayName = "Sent logs",  DisplayUnits = "logs" };
                _discardedLogsPc ??= new IncrementingEventCounter("discardedLogs", this) { DisplayName = "Discarded logs",  DisplayUnits = "logs" };
            }
            else if (args.Command == EventCommand.Disable)
            {
                Interlocked.Exchange(ref _receivedLogsPc, null)?.Dispose();
                Interlocked.Exchange(ref _sentLogsPc, null)?.Dispose();
                Interlocked.Exchange(ref _discardedLogsPc, null)?.Dispose();
            }
        }

        public void NotifyReceivedLog() => _receivedLogsPc?.Increment();
        public void NotifySentLog() => _sentLogsPc?.Increment();
        public void NotifyDiscardedLog() => _discardedLogsPc?.Increment();
    }
}
using System;
using System.Diagnostics.Tracing;
using System.Threading;

namespace SpikeDotnetCounters
{
    [EventSource(Name = "SpikeDotnetCounters")]
    internal sealed class SampleEventSource : EventSource
    {
        public static readonly SampleEventSource Instance = new SampleEventSource();

        private IncrementingEventCounter? _incomingCounter;
        private IncrementingEventCounter? _liveSentCounter;
        private IncrementingEventCounter? _liveDiscardedCounter;

        protected override void OnEventCommand(EventCommandEventArgs args)
        {
            if (args.Command == EventCommand.Enable)
            {
                _incomingCounter ??= new IncrementingEventCounter("receivedLogs", this) { DisplayName = "Incoming logs",  DisplayUnits = "logs" };
                _liveSentCounter ??= new IncrementingEventCounter("sentLogs", this) { DisplayName = "Sent logs",  DisplayUnits = "logs" };
                _liveDiscardedCounter ??= new IncrementingEventCounter("discardedLogs", this) { DisplayName = "Discarded logs",  DisplayUnits = "logs" };
            }
            else if (args.Command == EventCommand.Disable)
            {
                Interlocked.Exchange(ref _incomingCounter, null)?.Dispose();
                Interlocked.Exchange(ref _liveSentCounter, null)?.Dispose();
                Interlocked.Exchange(ref _liveDiscardedCounter, null)?.Dispose();
            }
        }

        public void NotifyReceivedLog() => _incomingCounter?.Increment();
        public void NotifySentLog() => _liveSentCounter?.Increment();
        public void NotifyDiscardedLog() => _liveDiscardedCounter?.Increment();
    }
}
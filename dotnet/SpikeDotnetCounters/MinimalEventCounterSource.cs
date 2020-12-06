using System;
using System.Diagnostics.Tracing;

namespace SpikeDotnetCounters
{
    [EventSource(Name = "SpikeDotnetCounters.MinimalEventCounterSource")]
    internal sealed class MinimalEventCounterSource : EventSource
    {
        public static readonly MinimalEventCounterSource Log = new MinimalEventCounterSource();

        private EventCounter _iterationTime;

        protected override void OnEventCommand(EventCommandEventArgs args)
        {
            Console.WriteLine(args.Command);
            if (args.Command == EventCommand.Enable)
            {
                _iterationTime ??= new EventCounter("iteration-time", this) {
                    DisplayName = "Iteration time",
                    DisplayUnits = "ms"
                };
            }
        }

        public void Iteration(float elapsedMilliseconds)
        {
            if (IsEnabled())
            {
                WriteEvent(1, elapsedMilliseconds);
                _iterationTime?.WriteMetric(elapsedMilliseconds);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _iterationTime?.Dispose();
            _iterationTime = null;
            
            base.Dispose(disposing);
        }
    }
}
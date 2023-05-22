using System.Diagnostics;
using System.Diagnostics.Tracing;

namespace MetricsSample;

[EventSource(Name = "Wrox.ProCSharp.MetricsSample")]
public class MetricsSampleSource : EventSource
{
    private IncrementingEventCounter? _totalRequestCounter;
    private IncrementingEventCounter? _errorCounter;
    private long _requestDuration;
#pragma warning disable IDE0052 // Remove unread private members
    private PollingCounter? _requestDurationCounter;
#pragma warning restore IDE0052 // Remove unread private members

    public static MetricsSampleSource Log { get; } = new();

    private MetricsSampleSource() : base("Wrox.ProCSharp.MetricsSample") { }

    protected override void OnEventCommand(EventCommandEventArgs command)
    {
        if (command.Command == EventCommand.Enable)
        {
            _totalRequestCounter ??= new("requests", this)
            {
                DisplayName = "Total requests",
                DisplayUnits = "Count",
                DisplayRateTimeScale = TimeSpan.FromSeconds(1),
            };

            _errorCounter ??= new("errors", this)
            {
                DisplayName = "Errors",
                DisplayUnits = "Count",
                DisplayRateTimeScale = TimeSpan.FromSeconds(1)
            };

            _requestDurationCounter
                ??= new("request-duration", this, () => Interlocked.Read(ref _requestDuration))
                {
                    DisplayName = "Request duration",
                    DisplayUnits = "ms"
                };
        }
    }

    public Stopwatch? RequestStart()
    {
        if (IsEnabled())
        {
            _totalRequestCounter?.Increment();
            return Stopwatch.StartNew();
        }
        else
        {
            return default;
        }
    }

    public void RequestStop(Stopwatch? stopwatch)
    {
        if (stopwatch is not null && stopwatch.IsRunning)
        {
            stopwatch.Stop();
            _ = Interlocked.Exchange(ref _requestDuration, stopwatch.ElapsedMilliseconds);
        }
    }

    public void Error()
    {
        if (IsEnabled())
        {
            _errorCounter?.Increment();
        }
    }
}

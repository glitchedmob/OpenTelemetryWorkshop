using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace OpenTelemetryWorkshop.Backend;

public static class DiagnosticConfig
{
    public const string ServiceName = "dotnet-backend";
    public static ActivitySource Source { get; } = new(ServiceName);
    public static Meter Meter { get; } = new(ServiceName, "0.0.1");
    public static Counter<int> ProfileRequests = Meter.CreateCounter<int>("profile_requests", "Number of profile requests");
    public static Histogram<int> RequestAge = Meter.CreateHistogram<int>("request_age", "Average age of profile requests");
}

using System.Diagnostics;

namespace OpenTelemetryWorkshop.Backend;

public static class RandomNumberSource
{
    public static ActivitySource Source { get; set; } = new ActivitySource("random-number");
}

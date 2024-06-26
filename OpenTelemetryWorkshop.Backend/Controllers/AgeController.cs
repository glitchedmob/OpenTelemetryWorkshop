using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;

namespace OpenTelemetryWorkshop.Backend.Controllers;

[ApiController]
[Route("age")]
public class AgeController(ILogger<AgeController> logger) : ControllerBase
{
    private static Random _randomAge = new();
    
    public ActionResult<int> GetAge([FromQuery] string firstname, [FromQuery] string surname)
    {
        Activity.Current?.SetTag("user_agent", Baggage.GetBaggage("original_user_agent"));

        var age = 0;
        using (var span = DiagnosticConfig.Source.StartActivity("Generate Age")) 
        {
            age = _randomAge.Next(18, 100);
            span?.SetTag("age", age);
        }
        logger.LogInformation("Generated age: {age}", age);
        
        DiagnosticConfig.ProfileRequests.Add(1, new("firstname", firstname), new("surname", surname));
        DiagnosticConfig.RequestAge.Record(age, new("firstname", firstname), new("surname", surname));
        
        return age;
    }
}
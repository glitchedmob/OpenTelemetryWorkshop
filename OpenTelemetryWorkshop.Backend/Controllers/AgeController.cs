using Microsoft.AspNetCore.Mvc;

namespace OpenTelemetryWorkshop.Backend.Controllers;

[ApiController]
[Route("age")]
public class AgeController(ILogger<AgeController> logger) : ControllerBase
{
    private static Random _randomAge = new();
    
    public ActionResult<int> GetAge()
    {
        var age = _randomAge.Next(18, 100);
        logger.LogInformation("Generated age: {age}", age);
        return age;
    }
}
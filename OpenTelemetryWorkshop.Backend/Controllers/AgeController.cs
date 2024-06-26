using Microsoft.AspNetCore.Mvc;

namespace OpenTelemetryWorkshop.Backend.Controllers;

[ApiController]
[Route("age")]
public class AgeController : ControllerBase
{
    private static Random _randomAge = new();
    
    public ActionResult<int> GetAge()
    {
        return _randomAge.Next(18, 100);
    }
}
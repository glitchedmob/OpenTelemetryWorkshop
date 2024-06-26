using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace OpenTelemetryWorkshop.Controllers;

[ApiController]
[Route("hello")]
public class HelloController(ILogger<HelloController> logger) : ControllerBase
{
    [HttpGet]
    public ActionResult<string> GetHello([FromQuery] string firstname, [FromQuery] string surname)
    {
        Activity.Current?.SetTag("firstname", firstname);
        Activity.Current?.SetTag("surname", surname);

        var fullname = $"{firstname} {surname}";
        
        logger.LogInformation($"Saying hello to {fullname}");

        return $"Hello {fullname}";
    }
}

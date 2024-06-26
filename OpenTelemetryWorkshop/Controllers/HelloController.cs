using Microsoft.AspNetCore.Mvc;

namespace OpenTelemetryWorkshop.Controllers;

[ApiController]
[Route("hello")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> GetHello([FromQuery] string firstName, [FromQuery] string surname)
    {
        return $"Hello {firstName} {surname}";
    }
}

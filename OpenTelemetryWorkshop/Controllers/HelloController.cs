using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace OpenTelemetryWorkshop.Controllers;

[ApiController]
[Route("hello")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> GetHello([FromQuery] string firstname, [FromQuery] string surname)
    {
        Activity.Current?.SetTag("firstname", firstname);
        Activity.Current?.SetTag("surname", surname);

        return $"Hello {firstname} {surname}";
    }
}

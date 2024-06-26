using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;

namespace OpenTelemetryWorkshop.Frontend.Controllers;

[ApiController]
[Route("hello")]
public class HelloController(ILogger<HelloController> logger, HttpClient httpClient, IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<string>> GetHello([FromQuery] string firstname, [FromQuery] string surname)
    {
        Activity.Current?.SetTag("firstname", firstname);
        Activity.Current?.SetTag("surname", surname);

        Baggage.SetBaggage("original_user_agent", HttpContext.Request.Headers["User-Agent"]);

        var fullname = $"{firstname} {surname}";
        logger.LogInformation("Saying hello to {fullname}", fullname);

        var backendBaseUrl = configuration["BackendBaseUrl"];
        var res = await httpClient.GetAsync($"{backendBaseUrl}/age?firstname={firstname}&surname={surname}");
        int.TryParse(await res.Content.ReadAsStringAsync(), out var age);

        return $"Hello {fullname} you are {age} years old";
    }
}

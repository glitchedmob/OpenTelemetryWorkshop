﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;

namespace OpenTelemetryWorkshop.Backend.Controllers;

[ApiController]
[Route("age")]
public class AgeController(ILogger<AgeController> logger) : ControllerBase
{
    private static Random _randomAge = new();
    
    public ActionResult<int> GetAge()
    {
        Activity.Current?.SetTag("user_agent", Baggage.GetBaggage("original_user_agent"));
        var age = _randomAge.Next(18, 100);
        logger.LogInformation("Generated age: {age}", age);
        return age;
    }
}
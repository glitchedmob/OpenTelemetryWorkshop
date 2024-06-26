using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetryWorkshop.Backend;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddOpenTelemetry(options => options.AddOtlpExporter());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => 
        resource
            .AddService("dotnet-backend")
            .AddAttributes(new List<KeyValuePair<string, object>>
            {
                new("developer", "Levi Zitting")
            }))
    .WithTracing(tpb =>
        tpb
            .AddSource(DiagnosticConfig.Source.Name)
            .AddAspNetCoreInstrumentation()
            .AddConsoleExporter()
            .AddOtlpExporter())
    .WithMetrics(mpb => 
        mpb
            .AddAspNetCoreInstrumentation()
            .AddMeter(DiagnosticConfig.Meter.Name)
            .AddOtlpExporter());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

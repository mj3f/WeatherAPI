using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(); // Important if you're not using Minimal APIs!
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Weather API",
        Description = "A simple example ASP.NET Core Web API - Inspired by Nick Chapsas video (see Contact url)",
        Contact = new OpenApiContact
        {
            Name = "Mike Pratt",
            Email = string.Empty,
            Url = new Uri("https://www.youtube.com/watch?v=A2pKhNQoQUU&t=32s"),
        },
        License = new OpenApiLicense
        {
            Name = "MIT"
        }
    });
});
builder.Services.AddOpenTelemetry()
    .WithMetrics(x =>
    {
        x.AddPrometheusExporter();
        x.AddMeter(
            "Microsoft.AspNetCore.Hosting", 
            "Microsoft.AspNetCore.Server.Kestrel",
            "System.Net.Http");

        x.AddView("request-duration", new ExplicitBucketHistogramConfiguration
        {
            Boundaries = new double[] { 0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 }
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();
app.MapPrometheusScrapingEndpoint(); // endpoint = /metrics by default.

app.Run();
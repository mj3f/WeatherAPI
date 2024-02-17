using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers;

[ApiController]
[Route("api/v1/weather")]
[Produces("application/json")]
public sealed class WeatherController(ILogger<WeatherController> logger) : ControllerBase
{
    private readonly string[] _summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
    
    [HttpGet("{city}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Description("Returns a list of all cities and their current forecast")]
    public async Task<IActionResult> Get([FromRoute] string city, [FromQuery] int days = 1)
    {
        var forecasts = Enumerable.Range(1, days).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    _summaries[Random.Shared.Next(_summaries.Length)]
                ))
            .ToArray();

        await Task.Delay(Random.Shared.Next(5, 100)); // Simulate external API call to fetch data...
        
        logger.LogInformation("Fetching weather forecast data for city {City}", city);
        
        return Ok(new CityWeatherForecast(city, forecasts.ToList()));
    }
}
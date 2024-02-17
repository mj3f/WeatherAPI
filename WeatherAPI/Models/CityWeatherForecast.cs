namespace WeatherAPI.Models;

public sealed record CityWeatherForecast(string City, List<WeatherForecast> Forecast);
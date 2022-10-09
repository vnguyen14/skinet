using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")] //client use this route. "controller" is just a place holder for the class's name is called
public class WeatherForecastController : ControllerBase //this controller is gonna tak ethe first part of the class name (weatherforecast) and that's the route. Example http/localhost:5000/weatherforcast
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    //If we send an http request to the controller:
    [HttpGet(Name = "GetWeatherForecast")] //this Attribute will execute the bellow function
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray(); //return an array of weather forecast (WeatherForecast.cs)
    }
}

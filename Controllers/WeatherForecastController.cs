using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherParser.DTOs;

namespace WeatherParser.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
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

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<WeatherResponse> GetAsync()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://api.weatherapi.com/v1/forecast.json?key=e45559308170413090a204018230603&q=ls25&days=2&aqi=no&alerts=no");

        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadAsStringAsync();

        var weatherData = JsonConvert.DeserializeObject<WeatherApiData>(data);

        var now = weatherData.Forecast.ForecastDay.First().Hour.ToArray()[DateTime.Now.Hour];

        Hour next = null;

        if(DateTime.Now.Hour < 23)
        {
            next = weatherData.Forecast.ForecastDay.First().Hour.ToArray()[DateTime.Now.Hour+1];
        }
        else
        {
            next = weatherData.Forecast.ForecastDay.ToArray()[1].Hour.ToArray()[0];
        }

        var output = new WeatherResponse 
        {
            DateTime = DateTime.Now,

            Now = new HourData
            {
                Time = now.Time,
                Code = now.Condition.Code,
                ChanceOfRain = now.Chance_Of_Rain,
                Temp = now.Temp_C
            },
            Next = new HourData
            {
                Time = next.Time,
                Code = next.Condition.Code,
                ChanceOfRain = next.Chance_Of_Rain,
                Temp = next.Temp_C
            }
        };

        return output;
    }
}

using Microsoft.AspNetCore.Mvc;
using Weather.Application;
using Weather.Application.Abstraction;
using Weather.Application.DTOs;
using Weather.Application.DTOs.Requests;
using Weather.Application.DTOs.Responses;
using Weather.Application.Services;

namespace Weather.Api.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherService _service;

    public WeatherForecastController(IWeatherService service) => _service = service;
    
    [HttpGet("daily")]
    public async Task<IActionResult> GetDailyWeather(
        [FromQuery] double latitude = 52.52,
        [FromQuery] double longitude = 13.41,
        CancellationToken cancellationToken = default
    )
    {
        DailyForecast? dailyForecast = await _service.GetDailyForecastAsync(
            new Coordinates(latitude, longitude), 
            new ForecastOptions(),
            cancellationToken
        );

        HourlyForecast? hourlyForecast = await _service.GetHourlyForecastAsync(
            new Coordinates(latitude, longitude), 
            new ForecastOptions(),
            cancellationToken
        );

        if (dailyForecast is null || hourlyForecast is null)
        {
            return StatusCode(502, new { error = "Open-Meteo request failed" });
        }
        
        ForecastResponse response = new ForecastResponse
        {
            Daily = dailyForecast,
            Hourly = hourlyForecast
        };

        return Ok(response);
    }
}
using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Weather.Application;
using Weather.Application.OpenMeteoDTOs;
using Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

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
        [FromQuery] int forecast_days = 3,
        [FromQuery] string timezone = "auto",
        CancellationToken ct = default
    )
    {
        var dto = await _service.GetDailyForecastAsync(
            new OpenMeteoWeatherDailyForecastRequest
            {
                Latitude = latitude,
                Longitude = longitude,
                ForecastDays = forecast_days,
                Timezone = timezone,
            }, 
            new OpenMeteoAirQualityHourlyRequest
            {
                Latitude = latitude,
                Longitude = longitude,
                ForecastDays = forecast_days,
                Timezone = timezone,
            },
            ct
        );

        return dto is null ? StatusCode(502, new { error = "Open-Meteo request failed" }) : Ok(dto);
    }
}
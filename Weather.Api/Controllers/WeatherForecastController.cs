using Microsoft.AspNetCore.Mvc;
using Weather.Application;
using Weather.Application.Abstraction;
using Weather.Application.DTOs;
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
        var dto = await _service.GetDailyForecastAsync(
            new Coordinates(latitude, longitude), 
            new ForecastOptions(),
            cancellationToken
        );

        return dto is null ? StatusCode(502, new { error = "Open-Meteo request failed" }) : Ok(dto);
    }
}
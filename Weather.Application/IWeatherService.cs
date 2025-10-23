using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs;
using Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Application;

public interface IWeatherService
{
    Task<DailyForecast?> GetDailyForecastAsync(
        double latitude,
        double longitude,
        CancellationToken ct
    );
}
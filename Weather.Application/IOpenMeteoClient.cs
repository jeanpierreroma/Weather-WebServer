using Weather.Application.OpenMeteoDTOs;
using Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Application;

public interface IOpenMeteoClient
{
    Task<OpenMeteoWeatherDailyForecastResponse?> GetDailyForecast(OpenMeteoWeatherDailyForecastRequest request, CancellationToken ct);
    Task<OpenMeteoAirQualityHourlyResponse?> GetHourlyAirQuality(OpenMeteoAirQualityHourlyRequest request, CancellationToken ct);
}
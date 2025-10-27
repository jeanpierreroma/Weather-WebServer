using Weather.Domain.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Domain.OpenMeteoDTOs.Settings;
using Weather.Domain.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Application.Abstraction;

public interface IWeatherProvider
{
    Task<OpenMeteoWeatherDailyForecastResponse?> GetDailyForecast(
        double latitude,
        double longitude,
        CancellationToken ct,
        int forecastDays = 1,
        string timezone = "auto",
        OpenMeteoSettings? settings = null,
        params WeatherDailyField[] dailyFields
    );

    Task<OpenMeteoAirQualityHourlyResponse?> GetHourlyAirQuality(
        double latitude,
        double longitude,
        CancellationToken ct,
        int forecastDays = 1,
        string timezone = "auto",
        OpenMeteoSettings? settings = null,
        params AirQualityHourlyField[] hourlyFields
    );
}
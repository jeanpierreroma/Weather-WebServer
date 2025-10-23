using Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;
using Weather.Application.OpenMeteoDTOs.Settings;

namespace Weather.Application;

public interface IOpenMeteoClient
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
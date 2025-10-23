using Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Application.OpenMeteoDTOs.Settings;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public static class OpenMeteoRequestBuilder
{
    public static OpenMeteoWeatherDailyForecastRequest BuildWeatherDailyForecastRequest(
        double latitude,
        double longitude,
        int forecastDays = 1,
        string timezone = "auto",
        OpenMeteoSettings? settings = null,
        params WeatherDailyField[] dailyFields
    )
    {
        // Якщо користувач не передав поля — беремо ВСІ з enum
        WeatherDailyField[] effectiveFields = dailyFields is { Length: > 0 }
            ? dailyFields
            : Enum.GetValues<WeatherDailyField>();
        
        string[] daily = effectiveFields
            .Select(f => f.ToApi())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        return new OpenMeteoWeatherDailyForecastRequest
        {
            Latitude = latitude,
            Longitude = longitude,
            ForecastDays = forecastDays,
            Timezone = timezone,
            Settings = settings,

            Daily = daily
        };
    }
    
    public static OpenMeteoAirQualityHourlyRequest BuildAirQualityHourlyRequest(
        double latitude,
        double longitude,
        int forecastDays = 1,
        string timezone = "auto",
        OpenMeteoSettings? settings = null,
        params AirQualityHourlyField[] hourlyFields
    )
    {
        // Якщо користувач не передав поля — беремо ВСІ з enum
        AirQualityHourlyField[] effectiveFields = hourlyFields is { Length: > 0 }
            ? hourlyFields
            : Enum.GetValues<AirQualityHourlyField>();
        
        string[] horuly = effectiveFields
            .Select(f => f.ToApi())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        return new OpenMeteoAirQualityHourlyRequest
        {
            Latitude = latitude,
            Longitude = longitude,
            ForecastDays = forecastDays,
            Timezone = timezone,
            Settings = settings,

            Hourly = horuly
        };
    }
}
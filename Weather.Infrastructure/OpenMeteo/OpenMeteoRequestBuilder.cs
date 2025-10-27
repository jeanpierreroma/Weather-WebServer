using Weather.Application.DTOs.ForecastSettings;
using Weather.Infrastructure.OpenMeteo.Mappers;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure.OpenMeteo;

public static class OpenMeteoRequestBuilder
{
    public static OpenMeteoWeatherDailyForecastRequest BuildWeatherDailyForecastRequest(
        double latitude,
        double longitude,
        int forecastDays = 1,
        string timezone = "auto",
        ForecastSetting? settings = null,
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
        
        var openMeteoSettings = settings is null
            ? null
            : OpenMeteoMapper.MapForecastOptionsToOpenMeteoSettings(settings);

        return new OpenMeteoWeatherDailyForecastRequest
        {
            Latitude = latitude,
            Longitude = longitude,
            ForecastDays = forecastDays,
            Timezone = timezone,
            Settings = openMeteoSettings,

            Daily = daily
        };
    }
    
    public static OpenMeteoAirQualityHourlyRequest BuildAirQualityHourlyRequest(
        double latitude,
        double longitude,
        int forecastDays = 1,
        string timezone = "auto",
        ForecastSetting? settings = null,
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
        
        var openMeteoSettings = settings is null
            ? null
            : OpenMeteoMapper.MapForecastOptionsToOpenMeteoSettings(settings);

        return new OpenMeteoAirQualityHourlyRequest
        {
            Latitude = latitude,
            Longitude = longitude,
            ForecastDays = forecastDays,
            Timezone = timezone,
            Settings = openMeteoSettings,

            Hourly = horuly
        };
    }
}
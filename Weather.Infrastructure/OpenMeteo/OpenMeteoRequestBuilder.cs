using Weather.Application.DTOs.ForecastSettings;
using Weather.Application.DTOs.Settings;
using Weather.Infrastructure.OpenMeteo.Mappers;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Settings;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Daily;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Hourly;

namespace Weather.Infrastructure.OpenMeteo;

public static class OpenMeteoRequestBuilder
{
    public static OpenMeteoWeatherForecastRequest BuildWeatherDailyForecastRequest(
        double latitude,
        double longitude,
        int forecastDays = 1,
        string timezone = "auto",
        ForecastSetting? settings = null
    )
    {
        OpenMeteoWeatherDailyFields[] dailyFields = Enum.GetValues<OpenMeteoWeatherDailyFields>();
        OpenMeteoWeatherHourlyFields[] hourlyFields = Enum.GetValues<OpenMeteoWeatherHourlyFields>();
        
        string[] daily = dailyFields
            .Select(f => f.ToApi())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
        
        string[] hourly = hourlyFields
            .Select(f => f.ToApi())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
        
        OpenMeteoSettings? openMeteoSettings = settings is null
            ? null
            : OpenMeteoMapper.MapForecastOptionsToOpenMeteoSettings(settings);

        return new OpenMeteoWeatherForecastRequest
        {
            Latitude = latitude,
            Longitude = longitude,
            ForecastDays = forecastDays,
            Timezone = timezone,
            Settings = openMeteoSettings,

            Daily = daily,
            Hourly = hourly
        };
    }
    
    public static OpenMeteoAirQualityHourlyRequest BuildAirQualityHourlyRequest(
        double latitude,
        double longitude,
        int forecastDays = 1,
        string timezone = "auto",
        ForecastSetting? settings = null
    )
    {
        AirQualityHourlyField[] effectiveFields = Enum.GetValues<AirQualityHourlyField>();
        
        string[] horuly = effectiveFields
            .Select(f => f.ToApi())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
        
        OpenMeteoSettings? openMeteoSettings = settings is null
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
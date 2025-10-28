using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Hourly;

namespace Weather.Infrastructure.OpenMeteo.Mappers;

public static class WeatherHourlyFieldMap
{
    public static string ToApi(this OpenMeteoWeatherHourlyFields f) => f switch
    {
        OpenMeteoWeatherHourlyFields.Temperature => "temperature_2m",
        OpenMeteoWeatherHourlyFields.ApparentTemperature => "apparent_temperature",
        OpenMeteoWeatherHourlyFields.UvIndex => "uv_index",
        OpenMeteoWeatherHourlyFields.Precipitation => "precipitation",
        OpenMeteoWeatherHourlyFields.Visibility => "visibility",
        OpenMeteoWeatherHourlyFields.WindDirection => "wind_direction_10m",
        OpenMeteoWeatherHourlyFields.WindGusts => "wind_speed_10m",
        OpenMeteoWeatherHourlyFields.WindSpeed => "wind_gusts_10m",
        OpenMeteoWeatherHourlyFields.RelativeHumidity => "relative_humidity_2m",
        OpenMeteoWeatherHourlyFields.SurfacePressure => "surface_pressure",
        
        _ => throw new ArgumentOutOfRangeException(nameof(f), f, null)
    };
}
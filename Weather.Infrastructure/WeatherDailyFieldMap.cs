using Weather.Application;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public static class WeatherDailyFieldMap
{
    public static string ToApi(this WeatherDailyField f) => f switch
    {
        WeatherDailyField.TemperatureMean => "temperature_2m_mean",
        WeatherDailyField.ApparentTemperatureMean => "apparent_temperature_mean",
        WeatherDailyField.Sunrise => "sunrise",
        WeatherDailyField.Sunset => "sunset",
        WeatherDailyField.UvIndexMax => "uv_index_max",
        WeatherDailyField.PrecipitationSum => "precipitation_sum",
        WeatherDailyField.VisibilityMean => "visibility_mean",
        WeatherDailyField.WindDirectionDominant => "winddirection_10m_dominant",
        WeatherDailyField.WindGustsMean => "wind_gusts_10m_mean",
        WeatherDailyField.WindSpeedMean => "wind_speed_10m_mean",
        WeatherDailyField.RelativeHumidityMean => "relative_humidity_2m_mean",
        WeatherDailyField.SurfacePressureMean => "surface_pressure_mean",
        
        _ => throw new ArgumentOutOfRangeException(nameof(f), f, null)
    };
}
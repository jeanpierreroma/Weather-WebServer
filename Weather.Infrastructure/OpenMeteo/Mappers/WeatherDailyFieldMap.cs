using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure.OpenMeteo.Mappers;

public static class WeatherDailyFieldMap
{
    public static string ToApi(this OpenMeteoWeatherDailyFields f) => f switch
    {
        OpenMeteoWeatherDailyFields.TemperatureMean => "temperature_2m_mean",
        OpenMeteoWeatherDailyFields.ApparentTemperatureMean => "apparent_temperature_mean",
        OpenMeteoWeatherDailyFields.Sunrise => "sunrise",
        OpenMeteoWeatherDailyFields.Sunset => "sunset",
        OpenMeteoWeatherDailyFields.UvIndexMax => "uv_index_max",
        OpenMeteoWeatherDailyFields.PrecipitationSum => "precipitation_sum",
        OpenMeteoWeatherDailyFields.VisibilityMean => "visibility_mean",
        OpenMeteoWeatherDailyFields.WindDirectionDominant => "winddirection_10m_dominant",
        OpenMeteoWeatherDailyFields.WindGustsMean => "wind_gusts_10m_mean",
        OpenMeteoWeatherDailyFields.WindSpeedMean => "wind_speed_10m_mean",
        OpenMeteoWeatherDailyFields.RelativeHumidityMean => "relative_humidity_2m_mean",
        OpenMeteoWeatherDailyFields.SurfacePressureMean => "surface_pressure_mean",
        
        _ => throw new ArgumentOutOfRangeException(nameof(f), f, null)
    };
}
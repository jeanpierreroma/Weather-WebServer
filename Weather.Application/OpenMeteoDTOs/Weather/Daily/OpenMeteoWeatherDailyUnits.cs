using System.Text.Json.Serialization;

namespace Weather.Application.OpenMeteoDTOs.Weather.Daily;

public sealed class OpenMeteoWeatherDailyUnits
{
    [JsonPropertyName("time")]
    public string Time { get; init; } = string.Empty;
    
    [JsonPropertyName("temperature_2m_mean")]
    public string TemperatureMean { get; init; } = string.Empty;
    
    [JsonPropertyName("apparent_temperature_mean")]
    public string ApparentTemperatureMean { get; init; } = string.Empty;
    
    [JsonPropertyName("sunrise")]
    public string Sunrise { get; init; } = string.Empty;

    [JsonPropertyName("sunset")]
    public string Sunset { get; init; } = string.Empty;

    [JsonPropertyName("uv_index_max")]
    public string UvIndex { get; init; } = string.Empty;

    [JsonPropertyName("precipitation_sum")]
    public string PrecipitationSum { get; init; } = string.Empty;
    
    [JsonPropertyName("visibility_mean")]
    public string VisibilityMean { get; init; } = string.Empty;
    
    [JsonPropertyName("winddirection_10m_dominant")]
    public string WindDirectionDominant { get; init; } = string.Empty;
    
    [JsonPropertyName("wind_gusts_10m_mean")]
    public string WindGustsMean { get; init; } = string.Empty;
    
    [JsonPropertyName("wind_speed_10m_mean")]
    public string WindSpeedMean { get; init; } = string.Empty;
    
    [JsonPropertyName("relative_humidity_2m_mean")]
    public string RelativeHumidityMean { get; init; } = string.Empty;
    
    [JsonPropertyName("surface_pressure_mean")]
    public string SurfacePressureMean { get; init; } = string.Empty;
}
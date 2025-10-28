using System.Text.Json.Serialization;

namespace Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Hourly;

public class OpenMeteoWeatherHourlyUnits
{
    [JsonPropertyName("time")]
    public string Time { get; init; } = string.Empty;
    
    [JsonPropertyName("temperature_2m")]
    public string Temperature { get; init; } = string.Empty;
    
    [JsonPropertyName("apparent_temperature")]
    public string ApparentTemperature { get; init; } = string.Empty;

    [JsonPropertyName("uv_index")]
    public string UvIndex { get; init; } = string.Empty;

    [JsonPropertyName("precipitation")]
    public string Precipitation { get; init; } = string.Empty;
    
    [JsonPropertyName("visibility")]
    public string Visibility { get; init; } = string.Empty;

    [JsonPropertyName("wind_direction_10m")]
    public string WindDirection { get; init; } = string.Empty;
    
    [JsonPropertyName("wind_gusts_10m")]
    public string WindGusts { get; init; } = string.Empty;
    
    [JsonPropertyName("wind_speed_10m")]
    public string WindSpeed { get; init; } = string.Empty;
    
    [JsonPropertyName("relative_humidity_2m")]
    public string RelativeHumidity { get; init; } = string.Empty;
    
    [JsonPropertyName("surface_pressure")]
    public string SurfacePressure { get; init; } = string.Empty;
}
using System.Text.Json.Serialization;

namespace Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Hourly;

public class OpenMeteoWeatherHourly
{
    [JsonPropertyName("time")]
    public List<string> Time { get; init; } = new();
    
    [JsonPropertyName("temperature_2m")]
    public List<double> Temperature { get; init; } = new();
    
    [JsonPropertyName("apparent_temperature")]
    public List<double> ApparentTemperature { get; init; } = new();

    [JsonPropertyName("uv_index")]
    public List<double> UvIndex { get; init; } = new();

    [JsonPropertyName("precipitation")]
    public List<double> Precipitation { get; init; } = new();
    
    [JsonPropertyName("visibility")]
    public List<double> Visibility { get; init; } = new();

    [JsonPropertyName("wind_direction_10m")]
    public List<int> WindDirection { get; init; } = new();
    
    [JsonPropertyName("wind_gusts_10m")]
    public List<double> WindGusts { get; init; } = new();
    
    [JsonPropertyName("wind_speed_10m")]
    public List<double> WindSpeed { get; init; } = new();
    
    [JsonPropertyName("relative_humidity_2m")]
    public List<int> RelativeHumidity { get; init; } = new();
    
    [JsonPropertyName("surface_pressure")]
    public List<double> SurfacePressure { get; init; } = new();
}
using System.Text.Json.Serialization;

namespace Weather.Application.OpenMeteoDTOs.Weather.Daily;

public sealed class OpenMeteoWeatherDaily
{
    [JsonPropertyName("time")]
    public List<string> Time { get; init; } = new();
    
    [JsonPropertyName("temperature_2m_mean")]
    public List<double?> TemperatureMean { get; init; } = new();
    
    [JsonPropertyName("apparent_temperature_mean")]
    public List<double?> ApparentTemperatureMean { get; init; } = new();
    
    [JsonPropertyName("sunrise")]
    public List<string> Sunrise { get; init; } = new();

    [JsonPropertyName("sunset")]
    public List<string> Sunset { get; init; } = new();

    [JsonPropertyName("uv_index_max")]
    public List<double?> UvIndexMax { get; init; } = new();

    [JsonPropertyName("precipitation_sum")]
    public List<double?> PrecipitationSum { get; init; } = new();
    
    [JsonPropertyName("visibility_mean")]
    public List<double?> VisibilityMean { get; init; } = new();

    [JsonPropertyName("winddirection_10m_dominant")]
    public List<int?> WindDirectionDominant { get; init; } = new();
    
    [JsonPropertyName("wind_gusts_10m_mean")]
    public List<double?> WindGustsMean { get; init; } = new();
    
    [JsonPropertyName("wind_speed_10m_mean")]
    public List<double?> WindSpeedMean { get; init; } = new();
    
    [JsonPropertyName("relative_humidity_2m_mean")]
    public List<int?> RelativeHumidityMean { get; init; } = new();
    
    [JsonPropertyName("surface_pressure_mean")]
    public List<double?> SurfacePressureMean { get; init; } = new();
}
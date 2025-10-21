using System.Text.Json.Serialization;

namespace Weather.Application.OpenMeteoDTOs;

public sealed class OpenMeteoDaily
{
    [JsonPropertyName("time")]
    public List<string> Time { get; init; } = new();

    [JsonPropertyName("uv_index_max")]
    public List<double?> UvIndexMax { get; init; } = new();

    [JsonPropertyName("sunrise")]
    public List<string> Sunrise { get; init; } = new();

    [JsonPropertyName("sunset")]
    public List<string> Sunset { get; init; } = new();

    [JsonPropertyName("precipitation_sum")]
    public List<double?> PrecipitationSum { get; init; } = new();

    [JsonPropertyName("wind_speed_10m_max")]
    public List<double?> WindSpeed10mMax { get; init; } = new();

    [JsonPropertyName("wind_gusts_10m_max")]
    public List<double?> WindGusts10mMax { get; init; } = new();

    [JsonPropertyName("wind_direction_10m_dominant")]
    public List<int?> WindDirection10mDominant { get; init; } = new();
}
using System.Text.Json.Serialization;

namespace Weather.Application.OpenMeteoDTOs.Daily;

public sealed class OpenMeteoDailyUnits
{
    [JsonPropertyName("time")]
    public string Time { get; init; } = string.Empty;

    [JsonPropertyName("uv_index_max")]
    public string UvIndexMax { get; init; } = string.Empty;

    [JsonPropertyName("sunrise")]
    public string Sunrise { get; init; } = string.Empty;

    [JsonPropertyName("sunset")]
    public string Sunset { get; init; } = string.Empty;

    [JsonPropertyName("precipitation_sum")]
    public string PrecipitationSum { get; init; } = string.Empty;

    [JsonPropertyName("wind_speed_10m_max")]
    public string WindSpeed10mMax { get; init; } = string.Empty;

    [JsonPropertyName("wind_gusts_10m_max")]
    public string WindGusts10mMax { get; init; } = string.Empty;

    [JsonPropertyName("wind_direction_10m_dominant")]
    public string WindDirection10mDominant { get; init; } = string.Empty;
}
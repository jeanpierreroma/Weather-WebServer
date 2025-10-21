using System.Text.Json.Serialization;

namespace Weather.Application.OpenMeteoDTOs;

public sealed class OpenMeteoDailyForecastResponse
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; init; }

    [JsonPropertyName("generationtime_ms")]
    public double GenerationTimeMs { get; init; }

    [JsonPropertyName("utc_offset_seconds")]
    public int UtcOffsetSeconds { get; init; }

    [JsonPropertyName("timezone")]
    public string Timezone { get; init; } = string.Empty;

    [JsonPropertyName("timezone_abbreviation")]
    public string TimezoneAbbreviation { get; init; } = string.Empty;

    [JsonPropertyName("elevation")]
    public double Elevation { get; init; }

    [JsonPropertyName("daily_units")]
    public OpenMeteoDailyUnits DailyUnits { get; init; } = new();

    [JsonPropertyName("daily")]
    public OpenMeteoDaily Daily { get; init; } = new();
}
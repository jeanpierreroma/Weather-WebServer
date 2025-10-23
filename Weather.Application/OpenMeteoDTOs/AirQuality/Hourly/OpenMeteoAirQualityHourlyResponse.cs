using System.Text.Json.Serialization;

namespace Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;

public class OpenMeteoAirQualityHourlyResponse
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
    
    [JsonPropertyName("hourly_units")]
    public OpenMeteoAirQualityHourlyUnits HourlyUnits { get; init; } = new();

    [JsonPropertyName("hourly")]
    public OpenMeteoAirQualityHourly Hourly { get; init; } = new();
}
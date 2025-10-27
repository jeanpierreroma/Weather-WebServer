using System.Text.Json.Serialization;

namespace Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.AirQuality.Hourly;

public class OpenMeteoAirQualityHourlyRequest : OpenMeteoRequest
{
    [JsonPropertyName("hourly")]
    public IReadOnlyList<string>? Hourly { get; init; }
}
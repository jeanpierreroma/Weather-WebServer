using System.Text.Json.Serialization;

namespace Weather.Domain.OpenMeteoDTOs.AirQuality.Hourly;

public class OpenMeteoAirQualityHourlyRequest : OpenMeteoRequest
{
    [JsonIgnore]
    public IReadOnlyList<string>? Hourly { get; init; }
}
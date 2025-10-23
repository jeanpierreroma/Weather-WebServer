using System.Text.Json.Serialization;

namespace Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;

public class OpenMeteoAirQualityHourlyRequest : OpenMeteoRequest
{
    [JsonIgnore]
    public IReadOnlyList<string>? Hourly { get; init; }
}
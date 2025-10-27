using System.Text.Json.Serialization;

namespace Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.AirQuality.Hourly;

public class OpenMeteoAirQualityHourlyResponse : OpenMeteoResponse
{
    [JsonPropertyName("hourly_units")]
    public OpenMeteoAirQualityHourlyUnits HourlyUnits { get; init; } = new();

    [JsonPropertyName("hourly")]
    public OpenMeteoAirQualityHourly Hourly { get; init; } = new();
}
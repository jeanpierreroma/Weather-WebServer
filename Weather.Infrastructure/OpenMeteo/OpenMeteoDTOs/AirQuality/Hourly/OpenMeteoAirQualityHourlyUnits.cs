using System.Text.Json.Serialization;

namespace Weather.Domain.OpenMeteoDTOs.AirQuality.Hourly;

public class OpenMeteoAirQualityHourlyUnits
{
    [JsonPropertyName("time")]
    public string Time { get; init; } = string.Empty;
    
    [JsonPropertyName("european_aqi")]
    public string EuropeanAqi { get; init; } = string.Empty;
}
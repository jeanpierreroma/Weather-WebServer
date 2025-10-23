using System.Text.Json.Serialization;

namespace Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;

public class OpenMeteoAirQualityHourly
{
    [JsonPropertyName("time")]
    public List<string> Time { get; init; } = new();
    
    [JsonPropertyName("european_aqi")]
    public List<int> EuropeanAqi { get; init; } = new();
}
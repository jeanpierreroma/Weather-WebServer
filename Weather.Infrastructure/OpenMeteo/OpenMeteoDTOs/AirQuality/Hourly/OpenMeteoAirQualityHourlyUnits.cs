using System.Text.Json.Serialization;

namespace Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.AirQuality.Hourly;

public class OpenMeteoAirQualityHourlyUnits
{
    [JsonPropertyName("time")]
    public string Time { get; init; } = string.Empty;
    
    [JsonPropertyName("european_aqi")]
    public string EuropeanAqi { get; init; } = string.Empty;
}
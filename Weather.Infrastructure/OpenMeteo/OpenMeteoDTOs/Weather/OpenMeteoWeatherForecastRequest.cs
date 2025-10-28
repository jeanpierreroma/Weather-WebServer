using System.Text.Json.Serialization;

namespace Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather;

public class OpenMeteoWeatherDailyForecastRequest : OpenMeteoRequest
{
    [JsonPropertyName("hourly")]
    public IReadOnlyList<string>? Hourly { get; init; }
    
    [JsonPropertyName("daily")]
    public IReadOnlyList<string>? Daily { get; init; }
}
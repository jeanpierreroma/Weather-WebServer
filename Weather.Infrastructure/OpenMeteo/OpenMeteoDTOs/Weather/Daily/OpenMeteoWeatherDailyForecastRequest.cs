using System.Text.Json.Serialization;

namespace Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Daily;

public class OpenMeteoWeatherDailyForecastRequest : OpenMeteoRequest
{
    [JsonPropertyName("daily")]
    public IReadOnlyList<string>? Daily { get; init; }
}
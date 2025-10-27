using System.Text.Json.Serialization;

namespace Weather.Domain.OpenMeteoDTOs.Weather.Daily;

public class OpenMeteoWeatherDailyForecastRequest : OpenMeteoRequest
{
    [JsonIgnore]
    public IReadOnlyList<string>? Daily { get; init; }
}
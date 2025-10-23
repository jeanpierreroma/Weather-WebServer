using System.Text.Json.Serialization;

namespace Weather.Application.OpenMeteoDTOs.Weather.Daily;

public class OpenMeteoWeatherDailyForecastRequest : OpenMeteoRequest
{
    [JsonIgnore]
    public IReadOnlyList<string>? Daily { get; init; }
}
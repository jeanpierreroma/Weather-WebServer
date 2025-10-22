using System.Text.Json.Serialization;

namespace Weather.Application.OpenMeteoDTOs.Daily;

public class OpenMeteoDailyForecastRequest
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; init; }
    
    [JsonPropertyName("longitude")]
    public double Longitude { get; init; }
    
    [JsonPropertyName("forecast_days")]
    public int ForecastDays { get; init; }
    
    [JsonPropertyName("timezone")]
    public string Timezone { get; init; } = null!;
    
    [JsonIgnore]
    public IReadOnlyList<string>? Daily { get; init; }
}
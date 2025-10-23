using System.Text.Json.Serialization;
using Weather.Application.OpenMeteoDTOs.Settings;

namespace Weather.Application.OpenMeteoDTOs;

public abstract class OpenMeteoRequest
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
    public OpenMeteoSettings? Settings { get; init; }
}
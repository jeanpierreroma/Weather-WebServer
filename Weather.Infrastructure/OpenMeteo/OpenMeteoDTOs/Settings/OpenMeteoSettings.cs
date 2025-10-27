using System.Text.Json.Serialization;

namespace Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Settings;

public class OpenMeteoSettings
{
    [JsonPropertyName("temperature_unit")] 
    public string TemperatureUnit { get; init; } = null!;
    
    [JsonPropertyName("wind_speed_unit")]
    public string WindSpeedUnit { get; init; } = null!;
    
    [JsonPropertyName("precipitation_unit")]
    public string PrecipitationUnit { get; init; } = null!;
    
    [JsonPropertyName("timeformat")]
    public string TimeFormatType { get; init; } = null!;
}
using System.Text.Json.Serialization;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Daily;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Hourly;

namespace Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather;

public sealed class OpenMeteoWeatherDailyForecastResponse : OpenMeteoResponse
{
    [JsonPropertyName("hourly_units")]
    public OpenMeteoWeatherHourlyUnits WeatherHourlyUnits { get; init; } = new();

    [JsonPropertyName("hourly")]
    public OpenMeteoWeatherHourly WeatherHourly { get; init; } = new();
    
    [JsonPropertyName("daily_units")]
    public OpenMeteoWeatherDailyUnits WeatherDailyUnits { get; init; } = new();

    [JsonPropertyName("daily")]
    public OpenMeteoWeatherDaily WeatherDaily { get; init; } = new();
}
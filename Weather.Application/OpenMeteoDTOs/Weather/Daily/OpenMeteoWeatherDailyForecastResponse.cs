using System.Text.Json.Serialization;

namespace Weather.Application.OpenMeteoDTOs.Weather.Daily;

public sealed class OpenMeteoWeatherDailyForecastResponse : OpenMeteoResponse
{
    [JsonPropertyName("daily_units")]
    public OpenMeteoWeatherDailyUnits WeatherDailyUnits { get; init; } = new();

    [JsonPropertyName("daily")]
    public OpenMeteoWeatherDaily WeatherDaily { get; init; } = new();
}
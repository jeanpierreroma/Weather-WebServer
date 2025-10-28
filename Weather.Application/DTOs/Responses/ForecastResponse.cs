namespace Weather.Application.DTOs.Responses;

public class ForecastResponse
{
    public required DailyForecast Daily { get; init; }
    public required HourlyForecast Hourly { get; init; }
}
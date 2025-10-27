using Weather.Application.DTOs;

namespace Weather.Application.Abstraction;

public interface IWeatherAggregator
{
    Task<ProcessedDailySections> ProcessAsync(ForecastData forecastData, CancellationToken ct);
}
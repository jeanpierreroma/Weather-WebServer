using Weather.Application.DTOs;

namespace Weather.Application.Abstraction;

public interface IForecastAggregator
{
    Task<ProcessedForecastSections> Aggregate(ForecastData forecastData, CancellationToken cancellationToken = default);
}
using Weather.Application.DTOs;
using Weather.Application.DTOs.Processed;
using Weather.Application.DTOs.Responses;

namespace Weather.Application.Abstraction;

public interface IForecastAggregator
{
    Task<ProcessedForecastSections> Aggregate(ForecastData forecastData, CancellationToken cancellationToken = default);
}
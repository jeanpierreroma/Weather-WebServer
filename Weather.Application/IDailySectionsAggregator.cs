using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Application;

public interface IDailySectionsAggregator
{
    Task<ProcessedDailySections> ProcessAsync(OpenMeteoWeatherDailyForecastResponse raw, CancellationToken ct);
}
using Weather.Application.DTOs;

namespace Weather.Application.Abstraction;

public interface IForecastProvider
{
    Task<ForecastData?> GetDailyForecast(
        Coordinates coordinates,
        ForecastOptions options,
        CancellationToken cancellationToken = default
    );
}
using Weather.Application.DTOs;

namespace Weather.Application.Abstraction;

public interface IWeatherService
{
    Task<DailyForecast?> GetDailyForecastAsync(
        Coordinates coordinates,
        ForecastOptions options,
        CancellationToken ct
    );
}
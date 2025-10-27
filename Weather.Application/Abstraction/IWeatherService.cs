using Weather.Domain.DTOs;

namespace Weather.Application.Services;

public interface IWeatherService
{
    Task<DailyForecast?> GetDailyForecastAsync(
        double latitude,
        double longitude,
        CancellationToken ct
    );
}
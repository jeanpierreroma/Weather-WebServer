using Weather.Application.DTOs;
using Weather.Application.DTOs.Requests;
using Weather.Application.DTOs.Responses;

namespace Weather.Application.Abstraction;

public interface IWeatherService
{
    Task<DailyForecast?> GetDailyForecastAsync(
        Coordinates coordinates,
        ForecastOptions options,
        CancellationToken ct
    );
}
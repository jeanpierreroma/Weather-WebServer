using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs;

namespace Weather.Application;

public interface IWeatherService
{
    Task<DailyForecast?> GetDailyForecastAsync(OpenMeteoDailyForecastRequest request, CancellationToken ct);
}
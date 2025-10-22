using Weather.Application.OpenMeteoDTOs;
using Weather.Application.OpenMeteoDTOs.Daily;

namespace Weather.Application;

public interface IOpenMeteoClient
{
    Task<OpenMeteoDailyForecastResponse?> GetDailyForecast(OpenMeteoDailyForecastRequest request, CancellationToken ct);
}
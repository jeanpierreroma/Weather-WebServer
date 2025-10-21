using Weather.Application.OpenMeteoDTOs;

namespace Weather.Application;

public interface IOpenMeteoClient
{
    Task<OpenMeteoDailyForecastResponse?> GetDailyForecast(OpenMeteoDailyForecastRequest request, CancellationToken ct);
}
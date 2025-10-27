using Weather.Application.DTOs;
using Weather.Application.DTOs.Requests;
using Weather.Application.DTOs.Responses;

namespace Weather.Application.Abstraction;

public interface IForecastProvider
{
    Task<ForecastData?> GetDailyForecast(
        Coordinates coordinates,
        ForecastOptions options,
        CancellationToken cancellationToken = default
    );
}
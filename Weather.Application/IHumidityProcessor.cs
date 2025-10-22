using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs;
using Weather.Application.OpenMeteoDTOs.Daily;

namespace Weather.Application;

public interface IHumidityProcessor
{
    HumidityDetails Process(OpenMeteoDailyForecastResponse raw);
}
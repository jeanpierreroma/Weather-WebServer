using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs;

namespace Weather.Application;

public interface IUvIndexProcessor
{
    UvIndexDetails Process(OpenMeteoDailyForecastResponse raw);
}
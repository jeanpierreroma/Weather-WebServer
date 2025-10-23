using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public class VisibilityProcessor : IVisibilityProcessor
{
    public VisibilityDetails Process(OpenMeteoWeatherDailyForecastResponse raw)
    {
        throw new NotImplementedException();
    }
}
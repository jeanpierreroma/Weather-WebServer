using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public class PrecipitationProcessor : IPrecipitationProcessor
{
    public PrecipitationDetails Process(OpenMeteoWeatherDailyForecastResponse raw)
    {
        throw new NotImplementedException();
    }
}
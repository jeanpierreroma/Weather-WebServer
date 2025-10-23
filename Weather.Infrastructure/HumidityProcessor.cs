using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public class HumidityProcessor : IHumidityProcessor
{
    public HumidityDetails Process(OpenMeteoWeatherDailyForecastResponse raw)
    {
        throw new NotImplementedException();
    }
}
using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public class PressureProcessor : IPressureProcessor
{
    public PressureDetails Process(OpenMeteoWeatherDailyForecastResponse raw)
    {
        throw new NotImplementedException();
    }
}
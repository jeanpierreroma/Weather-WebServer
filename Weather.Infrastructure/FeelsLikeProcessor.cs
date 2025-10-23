using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public class FeelsLikeProcessor : IFeelsLikeProcessor
{
    public FeelsLikeDetails Process(OpenMeteoWeatherDailyForecastResponse raw)
    {
        throw new NotImplementedException();
    }
}
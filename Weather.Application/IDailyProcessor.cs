using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Application;

public interface IDailyProcessor<out TOut>
    : IProcessor<OpenMeteoWeatherDailyForecastResponse, TOut>
{
    
}

using Weather.Application.DTOs;

namespace Weather.Application.Processors;

public interface IDailyProcessor<out TOut>
    : IProcessor<ForecastData, TOut>
{
    
}

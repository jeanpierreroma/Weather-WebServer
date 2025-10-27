using Weather.Application.DTOs;

namespace Weather.Application.Processors;

public interface IForecastProcessor<out TOut>
    : IProcessor<ForecastData, TOut>
{
    
}

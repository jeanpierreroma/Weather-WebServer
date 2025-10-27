using Weather.Application.DTOs;
using Weather.Application.DTOs.Responses;

namespace Weather.Application.Processors;

public interface IForecastProcessor<out TOut>
    : IProcessor<ForecastData, TOut>
{
    
}

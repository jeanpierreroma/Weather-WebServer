using Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;

namespace Weather.Application;

public interface IHourlyProcessor<out TOut> : IProcessor<OpenMeteoAirQualityHourlyResponse, TOut>
{
    
}
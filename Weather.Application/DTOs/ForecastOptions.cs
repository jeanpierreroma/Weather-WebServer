using Weather.Application.DTOs.ForecastSettings;

namespace Weather.Application.DTOs;

public record ForecastOptions(int ForecastDays, string Timezone, ForecastSetting Settings)
{
    public ForecastOptions() 
        : this(1, "auto", ForecastSetting.Default) { }
    
    public ForecastOptions(int forecastDays, string timezone)
        : this(forecastDays, timezone, ForecastSetting.Default) { }
}
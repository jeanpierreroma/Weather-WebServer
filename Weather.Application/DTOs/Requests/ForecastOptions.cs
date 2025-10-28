using Weather.Application.DTOs.ForecastSettings;
using Weather.Application.DTOs.Settings;

namespace Weather.Application.DTOs.Requests;

public record ForecastOptions(int ForecastDays, string Timezone, ForecastSetting Settings)
{
    public ForecastOptions() 
        : this(1, "auto", ForecastSetting.Default) { }
    
    public ForecastOptions(int forecastDays, string timezone)
        : this(forecastDays, timezone, ForecastSetting.Default) { }
}
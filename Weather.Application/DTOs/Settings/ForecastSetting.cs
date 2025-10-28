using Weather.Application.DTOs.ForecastSettings;

namespace Weather.Application.DTOs.Settings;

public record ForecastSetting(
    TemperatureUnit TemperatureUnit,
    WindSpeedUnit WindSpeedUnit,
    PrecipitationUnit PrecipitationUnit,
    TimeFormat TimeFormat
)
{
    public static readonly ForecastSetting Default = new(
        TemperatureUnit.Celsius,
        WindSpeedUnit.Kmh,
        PrecipitationUnit.Millimeter,
        TimeFormat.Iso8601
    );
}
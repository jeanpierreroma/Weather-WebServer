namespace Weather.Application.DTOs.ForecastSettings;

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
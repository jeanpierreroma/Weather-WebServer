using Weather.Domain.OpenMeteoDTOs.Settings;

namespace Weather.Application.Mappers;

public static class UnitsMap
{
    public static string ToApi(this TemperatureUnit u) => 
        u == TemperatureUnit.Celsius ? "celsius" : "fahrenheit";
    
    public static string ToApi(this WindSpeedUnit u) => u switch
    {
        WindSpeedUnit.Ms => "ms", 
        WindSpeedUnit.Kmh => "kmh", 
        WindSpeedUnit.Mph => "mph", 
        WindSpeedUnit.Kn => "kn", 
        _ => "kmh"
    };
    public static string ToApi(this PrecipitationUnit u) => 
        u == PrecipitationUnit.Inch ? "inch" : "mm";
    public static string ToApi(this TimeFormat f) => 
        f == TimeFormat.UnixTimestamp ? "unixtime" : "iso8601";
}
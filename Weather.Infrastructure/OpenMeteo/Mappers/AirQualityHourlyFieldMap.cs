using Weather.Domain.OpenMeteoDTOs.AirQuality.Hourly;

namespace Weather.Application.Mappers;

public static class AirQualityHourlyFieldMap
{
    public static string ToApi(this AirQualityHourlyField f) => f switch
    {
        AirQualityHourlyField.EuropeanAqi => "european_aqi",
        _ => throw new ArgumentOutOfRangeException(nameof(f), f, null)
    };
}
using System.Globalization;
using Weather.Application.DTOs;
using Weather.Application.DTOs.ForecastSettings;
using Weather.Application.DTOs.Responses;
using Weather.Application.DTOs.Settings;
using Weather.Domain.ValueObjects;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Settings;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Daily;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Hourly;

namespace Weather.Infrastructure.OpenMeteo.Mappers;

public static class OpenMeteoMapper
{
    public static Daily MapOpenMeteoWeatherDailyForecastResponseToDaily(OpenMeteoWeatherForecastResponse weatherForecastResponse)
    {
        OpenMeteoWeatherDaily weatherDaily = weatherForecastResponse.WeatherDaily;

        Daily daily = new Daily(
            TemperatureMean:         weatherDaily.TemperatureMean,
            ApparentTemperatureMean: weatherDaily.ApparentTemperatureMean,
            Sunrise:                 weatherDaily.Sunrise,
            Sunset:                  weatherDaily.Sunset,
            UvIndexMax:              weatherDaily.UvIndexMax,
            PrecipitationSum:        weatherDaily.PrecipitationSum,
            VisibilityMean:          weatherDaily.VisibilityMean,
            WindDirectionDominant:   weatherDaily.WindDirectionDominant,
            WindGustsMean:           weatherDaily.WindGustsMean,
            WindSpeedMean:           weatherDaily.WindSpeedMean,
            RelativeHumidityMean:    weatherDaily.RelativeHumidityMean,
            SurfacePressureMean:     weatherDaily.SurfacePressureMean,
            MoonSnapshot:            null
        );

        return daily;
    }

    public static Hourly MapOpenMeteoAirQualityHourlyResponseToHourly(
        OpenMeteoAirQualityHourlyResponse airQualityHourlyResponse,
        OpenMeteoWeatherForecastResponse weatherForecastResponse
    )
    {
        OpenMeteoAirQualityHourly airQualityHourly = airQualityHourlyResponse.Hourly;
        OpenMeteoWeatherHourly weatherDailyForecast = weatherForecastResponse.WeatherHourly;
        
        Hourly hourly = new Hourly(
            Time: ParseOpenMeteoTimesToUtc(weatherDailyForecast.Time),
            EuropeanAqi: airQualityHourly.EuropeanAqi,
            Temperature: weatherDailyForecast.Temperature,
            ApparentTemperature: weatherDailyForecast.ApparentTemperature,
            UvIndex: weatherDailyForecast.UvIndex,
            Precipitation: weatherDailyForecast.Precipitation,
            Visibility: weatherDailyForecast.Visibility,
            WindDirection: weatherDailyForecast.WindDirection,
            WindGusts: weatherDailyForecast.WindGusts,
            WindSpeed: weatherDailyForecast.WindSpeed,
            RelativeHumidity: weatherDailyForecast.RelativeHumidity,
            SurfacePressure: weatherDailyForecast.SurfacePressure
        );

        return hourly;
    }

    public static OpenMeteoSettings MapForecastOptionsToOpenMeteoSettings(ForecastSetting settings)
    {
        return new OpenMeteoSettings
        {
            TemperatureUnit = settings.TemperatureUnit.ToApi(),
            WindSpeedUnit = settings.WindSpeedUnit.ToApi(),
            PrecipitationUnit = settings.PrecipitationUnit.ToApi(),
            TimeFormatType = settings.TimeFormat.ToApi()
        };
    }
    
    private static List<DateTime> ParseOpenMeteoTimesToUtc(
        IReadOnlyList<string> times
    )
    {
        var result = new List<DateTime>(times.Count);

        foreach (var s in times)
        {
            if (DateTimeOffset.TryParse(
                    s,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                    out var dto))
            {
                result.Add(dto.UtcDateTime);
                continue;
            }

            if (DateTime.TryParseExact(
                    s,
                    "yyyy-MM-dd'T'HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var localNoKind))
            {
                var unspecified = DateTime.SpecifyKind(localNoKind, DateTimeKind.Unspecified);

                result.Add(unspecified);
            }
        }

        return result;
    }
}
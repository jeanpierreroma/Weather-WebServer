using Weather.Application.DTOs;
using Weather.Application.DTOs.ForecastSettings;
using Weather.Infrastructure.OpenMeteo.Mappers;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Settings;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs;

public static class OpenMeteoMapper
{
    public static Daily MapOpenMeteoWeatherDailyForecastResponseToDaily(OpenMeteoWeatherDailyForecastResponse weatherDailyForecastResponse)
    {
        OpenMeteoWeatherDaily weatherDaily = weatherDailyForecastResponse.WeatherDaily;

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
            SurfacePressureMean:     weatherDaily.SurfacePressureMean
        );

        return daily;
    }

    public static Hourly MapOpenMeteoAirQualityHourlyResponseToHourly(OpenMeteoAirQualityHourlyResponse airQualityHourlyResponse)
    {
        OpenMeteoAirQualityHourly airQualityHourly = airQualityHourlyResponse.Hourly;
        
        Hourly hourly = new Hourly(
            EuropeanAqi: airQualityHourly.EuropeanAqi
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
}
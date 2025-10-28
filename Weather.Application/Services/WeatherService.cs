using Weather.Application.Abstraction;
using Weather.Application.DTOs;
using Weather.Application.DTOs.Processed;
using Weather.Application.DTOs.Requests;
using Weather.Application.DTOs.Responses;
using Weather.Application.DTOs.Responses.Details;

namespace Weather.Application.Services;

public class WeatherService: IWeatherService
{
    private readonly IForecastProvider _forecastClient;
    private readonly IMoonProvider _moonClient;
    private readonly IForecastAggregator _dailyAggregator;
    
    public WeatherService(
        IForecastProvider forecastClient, 
        IMoonProvider moonClient,
        IForecastAggregator dailyAggregator)
    {
        _forecastClient = forecastClient;
        _moonClient = moonClient;
        _dailyAggregator = dailyAggregator;
    }    
    
    public async Task<DailyForecast?> GetDailyForecastAsync(Coordinates coordinates, ForecastOptions options, CancellationToken cancellationToken)
    {
        ForecastData? weatherForecastResponse = await _forecastClient.GetDailyForecast(
            coordinates: coordinates,
            options: options,
            cancellationToken: cancellationToken
        );
        
        if (weatherForecastResponse is null) return null;
        
        MoonSnapshotDto? moonSnapshot = await _moonClient.GetMoon(
            dateTime: DateTime.UtcNow,
            cancellationToken: cancellationToken
        );

        weatherForecastResponse = weatherForecastResponse with
        {
            Daily = weatherForecastResponse.Daily with { MoonSnapshot = moonSnapshot }
        };

        ProcessedForecastSections forecastSections = await _dailyAggregator.Aggregate(weatherForecastResponse, cancellationToken);
        
        DailyForecast dailyForecast = new DailyForecast
        {
            AirQualityDetails = forecastSections.AirQuality,
            FeelsLikeDetails = forecastSections.FeelsLike,
            HumidityDetails = forecastSections.Humidity,
            PrecipitationDetails = forecastSections.Precipitation,
            PressureDetails = forecastSections.Pressure,
            SunDetails = new SunDetails
            {
                SunriseText = weatherForecastResponse.Daily.Sunrise.FirstOrDefault() ?? string.Empty,
                SunsetText = weatherForecastResponse.Daily.Sunset.FirstOrDefault() ?? string.Empty,
            },
            UvDetails = forecastSections.Uv,
            VisibilityDetails = forecastSections.Visibility,
            MoonDetails = forecastSections.Moon,
            WindDetails = new WindDetails
            {
                WindSpeedMps = weatherForecastResponse.Daily.WindSpeedMean?.FirstOrDefault() ?? 0,
                GustSpeedMps = weatherForecastResponse.Daily.WindGustsMean?.FirstOrDefault() ?? 0,
                DirectionDegrees = weatherForecastResponse.Daily.WindDirectionDominant?.FirstOrDefault() ?? 0
            }
        };

        return dailyForecast;
    }

    public async Task<HourlyForecast?> GetHourlyForecastAsync(Coordinates coordinates, ForecastOptions options, CancellationToken cancellationToken)
    {
        ForecastData? weatherForecastResponse = await _forecastClient.GetDailyForecast(
            coordinates: coordinates,
            options: options,
            cancellationToken: cancellationToken
        );
        
        if (weatherForecastResponse is null) return null;
        
        HourlyForecast hourlyForecast = new HourlyForecast
        {
            Temperature = ToHourPoints(weatherForecastResponse.Hourly.Time, weatherForecastResponse.Hourly.Temperature),
            ApparentTemperature = ToHourPoints(weatherForecastResponse.Hourly.Time, weatherForecastResponse.Hourly.ApparentTemperature),
            UvIndex = ToHourPoints(weatherForecastResponse.Hourly.Time, weatherForecastResponse.Hourly.UvIndex),
            Precipitation = ToHourPoints(weatherForecastResponse.Hourly.Time, weatherForecastResponse.Hourly.Precipitation),
            Visibility = ToHourPoints(weatherForecastResponse.Hourly.Time, weatherForecastResponse.Hourly.Visibility),
            WindDirection = ToHourPoints<int>(weatherForecastResponse.Hourly.Time, weatherForecastResponse.Hourly.WindDirection),
            WindGusts = ToHourPoints(weatherForecastResponse.Hourly.Time, weatherForecastResponse.Hourly.WindGusts),
            WindSpeed = ToHourPoints(weatherForecastResponse.Hourly.Time, weatherForecastResponse.Hourly.WindSpeed),
            RelativeHumidity = ToHourPoints<int>(weatherForecastResponse.Hourly.Time, weatherForecastResponse.Hourly.RelativeHumidity),
            SurfacePressure = ToHourPoints(weatherForecastResponse.Hourly.Time, weatherForecastResponse.Hourly.SurfacePressure),
            EuropeanAqi = ToHourPoints<int>(weatherForecastResponse.Hourly.Time, weatherForecastResponse.Hourly.EuropeanAqi),
        };

        return hourlyForecast;
    }
    
    private static IReadOnlyList<HourPoint<T>> ToHourPoints<T>(
        IReadOnlyList<DateTime>? time,
        IReadOnlyList<T>? values)
    {
        if (time is null || values is null) return [];

        int n = Math.Min(time.Count, values.Count);
        var list = new List<HourPoint<T>>(n);
        for (int i = 0; i < n; i++)
        {
            list.Add(new HourPoint<T>(time[i], values[i]));
        }
        return list;
    }
}
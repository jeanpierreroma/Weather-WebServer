using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public class WeatherService: IWeatherService
{
    private readonly IOpenMeteoClient _client;
    private readonly IAirQualityProcessor _airQualityProcessor;
    private readonly IDailySectionsAggregator _dailyAggregator;
    
    public WeatherService(
        IOpenMeteoClient client, 
        IAirQualityProcessor airQualityProcessor, 
        IDailySectionsAggregator dailyAggregator)
    {
        _client = client;
        _airQualityProcessor = airQualityProcessor;
        _dailyAggregator = dailyAggregator;
    }    
    
    public async Task<DailyForecast?> GetDailyForecastAsync(double latitude, double longitude, CancellationToken ct)
    {
        Task<OpenMeteoWeatherDailyForecastResponse?> weatherDailyForecastTask = _client.GetDailyForecast(
            latitude: latitude,
            longitude: longitude,
            ct: ct
        );
        
        Task<OpenMeteoAirQualityHourlyResponse?> airQualityHourlyTask = _client.GetHourlyAirQuality(
            latitude: latitude,
            longitude: longitude,
            ct: ct
        );
        
        await Task.WhenAll(weatherDailyForecastTask, airQualityHourlyTask);
        
        OpenMeteoWeatherDailyForecastResponse? weatherDailyForecastResponse = weatherDailyForecastTask.Result;
        OpenMeteoAirQualityHourlyResponse? airQualityHourlyResponse = airQualityHourlyTask.Result;
        
        if (weatherDailyForecastResponse is null || airQualityHourlyResponse is null) return null;
        
        AirQualityDetails airQualitySection = _airQualityProcessor.Process(airQualityHourlyResponse);
        ProcessedDailySections dailySections = await _dailyAggregator.ProcessAsync(weatherDailyForecastResponse, ct);
        
        return new DailyForecast
        {
            AirQualityDetails = airQualitySection,
            FeelsLikeDetails = dailySections.FeelsLike,
            HumidityDetails = dailySections.Humidity,
            PrecipitationDetails = dailySections.Precipitation,
            PressureDetails = dailySections.Pressure,
            SunDetails = new SunDetails
            {
                SunriseText = weatherDailyForecastResponse.WeatherDaily.Sunrise.FirstOrDefault() ?? string.Empty,
                SunsetText = weatherDailyForecastResponse.WeatherDaily.Sunset.FirstOrDefault() ?? string.Empty,
            },
            UvDetails = dailySections.Uv,
            VisibilityDetails = dailySections.Visibility,
            WindDetails = new WindDetails
            {
                WindSpeedMps = weatherDailyForecastResponse.WeatherDaily.WindSpeedMean?.FirstOrDefault() ?? 0,
                GustSpeedMps = weatherDailyForecastResponse.WeatherDaily.WindGustsMean?.FirstOrDefault() ?? 0,
                DirectionDegrees = weatherDailyForecastResponse.WeatherDaily.WindDirectionDominant?.FirstOrDefault() ?? 0
            }
        };
    }
}
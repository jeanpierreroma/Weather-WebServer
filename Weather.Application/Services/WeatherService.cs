using Weather.Application.Abstraction;
using Weather.Application.DTOs;

namespace Weather.Application.Services;

public class WeatherService: IWeatherService
{
    private readonly IForecastProvider _client;
    private readonly IForecastAggregator _dailyAggregator;
    
    public WeatherService(
        IForecastProvider client, 
        IForecastAggregator dailyAggregator)
    {
        _client = client;
        _dailyAggregator = dailyAggregator;
    }    
    
    public async Task<DailyForecast?> GetDailyForecastAsync(Coordinates coordinates, ForecastOptions options, CancellationToken cancellationToken)
    {
        ForecastData? weatherForecastResponse = await _client.GetDailyForecast(
            coordinates: coordinates,
            options: options,
            cancellationToken: cancellationToken
        );
        
        if (weatherForecastResponse is null) return null;
        
        ProcessedForecastSections forecastSections = await _dailyAggregator.Aggregate(weatherForecastResponse, cancellationToken);
        
        return new DailyForecast
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
            WindDetails = new WindDetails
            {
                WindSpeedMps = weatherForecastResponse.Daily.WindSpeedMean?.FirstOrDefault() ?? 0,
                GustSpeedMps = weatherForecastResponse.Daily.WindGustsMean?.FirstOrDefault() ?? 0,
                DirectionDegrees = weatherForecastResponse.Daily.WindDirectionDominant?.FirstOrDefault() ?? 0
            }
        };
    }
}
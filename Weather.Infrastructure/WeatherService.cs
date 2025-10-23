using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public class WeatherService: IWeatherService
{
    private static readonly string[] RequiredDaily =
    [
        "uv_index_max",
        "precipitation_sum",
        "wind_speed_10m_max",
        "wind_gusts_10m_max",
        "wind_direction_10m_dominant"
    ];
    
    private readonly IOpenMeteoClient _client;

    private readonly IAirQualityProcessor _airQualityProcessor;
    private readonly IFeelsLikeProcessor _feelsLikeProcessor;
    private readonly IHumidityProcessor _humidityProcessor;
    private readonly IPrecipitationProcessor _precipitationProcessor;
    private readonly IPressureProcessor _pressureProcessor;
    private readonly IUvProcessor _uvProcessor;
    private readonly IVisibilityProcessor _visibilityProcessor;
    
    public WeatherService(IOpenMeteoClient client, IUvProcessor uvProcessor, IAirQualityProcessor airQualityProcessor, IFeelsLikeProcessor feelsLikeProcessor, IHumidityProcessor humidityProcessor, IPrecipitationProcessor precipitationProcessor, IPressureProcessor pressureProcessor, IVisibilityProcessor visibilityProcessor)
    {
        _client = client;
        
        _airQualityProcessor = airQualityProcessor;
        _feelsLikeProcessor = feelsLikeProcessor;
        _humidityProcessor = humidityProcessor;
        _precipitationProcessor = precipitationProcessor;
        _pressureProcessor = pressureProcessor;
        _uvProcessor = uvProcessor;
        _visibilityProcessor = visibilityProcessor;
    }    
    
    public async Task<DailyForecast?> GetDailyForecastAsync(
        OpenMeteoWeatherDailyForecastRequest weatherDailyForecastRequest, 
        OpenMeteoAirQualityHourlyRequest airQualityHourlyRequest,
        CancellationToken ct)
    {
        var daily = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        if (weatherDailyForecastRequest.Daily is { Count: > 0 })
            foreach (var d in weatherDailyForecastRequest.Daily) daily.Add(d);

        foreach (var r in RequiredDaily) daily.Add(r);

        var weatherDailyForecastReq = new OpenMeteoWeatherDailyForecastRequest
        {
            Latitude = weatherDailyForecastRequest.Latitude,
            Longitude = weatherDailyForecastRequest.Longitude,
            ForecastDays = weatherDailyForecastRequest.ForecastDays,
            Timezone = string.IsNullOrWhiteSpace(weatherDailyForecastRequest.Timezone) ? "auto" : weatherDailyForecastRequest.Timezone,
            Daily = daily.ToArray()
        };
        
        var weatherDailyForecastResponse = await _client.GetDailyForecast(weatherDailyForecastRequest, ct);
        if (weatherDailyForecastResponse is null) return null;
        
        var hourly = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        if (airQualityHourlyRequest.Hourly is { Count: > 0 })
            foreach (var h in airQualityHourlyRequest.Hourly) hourly.Add(h);

        foreach (var r in RequiredDaily) daily.Add(r);

        var airQualityHourlyReq = new OpenMeteoAirQualityHourlyRequest
        {
            Latitude = airQualityHourlyRequest.Latitude,
            Longitude = airQualityHourlyRequest.Longitude,
            ForecastDays = airQualityHourlyRequest.ForecastDays,
            Timezone = string.IsNullOrWhiteSpace(airQualityHourlyRequest.Timezone)
                ? "auto"
                : airQualityHourlyRequest.Timezone,
            Hourly = hourly.ToArray()
        };

        var airQualityHourlyResponse = await _client.GetHourlyAirQuality(airQualityHourlyReq, ct);
        if (airQualityHourlyResponse is null) return null;
        
        var airQualitySection = _airQualityProcessor.Process(airQualityHourlyResponse);
        var feelsLikeSection = _feelsLikeProcessor.Process(weatherDailyForecastResponse);
        var humiditySection = _humidityProcessor.Process(weatherDailyForecastResponse);
        var precipitationSection = _precipitationProcessor.Process(weatherDailyForecastResponse);
        var pressureSection = _pressureProcessor.Process(weatherDailyForecastResponse);
        var uvSection = _uvProcessor.Process(weatherDailyForecastResponse);
        var visibilitySection = _visibilityProcessor.Process(weatherDailyForecastResponse);
        
        return new DailyForecast
        {
            AirQualityDetails = airQualitySection,
            FeelsLikeDetails = feelsLikeSection,
            HumidityDetails = humiditySection,
            PrecipitationDetails = precipitationSection,
            PressureDetails = pressureSection,
            SunDetails = new SunDetails
            {
                SunriseText = weatherDailyForecastResponse.WeatherDaily.Sunrise.FirstOrDefault() ?? string.Empty,
                SunsetText = weatherDailyForecastResponse.WeatherDaily.Sunset.FirstOrDefault() ?? string.Empty,
            },
            UvDetails = uvSection,
            VisibilityDetails = visibilitySection,
            WindDetails = new WindDetails
            {
                WindSpeedMps = weatherDailyForecastResponse.WeatherDaily.WindSpeedMean?.FirstOrDefault() ?? 0,
                GustSpeedMps = weatherDailyForecastResponse.WeatherDaily.WindGustsMean?.FirstOrDefault() ?? 0,
                DirectionDegrees = weatherDailyForecastResponse.WeatherDaily.WindDirectionDominant?.FirstOrDefault() ?? 0
            }
        };
    }
}
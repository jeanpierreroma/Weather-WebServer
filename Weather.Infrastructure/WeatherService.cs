using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs;
using Weather.Application.OpenMeteoDTOs.Daily;

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
    
    public async Task<DailyForecast?> GetDailyForecastAsync(OpenMeteoDailyForecastRequest request, CancellationToken ct)
    {
        var daily = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        if (request.Daily is { Count: > 0 })
            foreach (var d in request.Daily) daily.Add(d);

        foreach (var r in RequiredDaily) daily.Add(r);

        var req = new OpenMeteoDailyForecastRequest
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            ForecastDays = request.ForecastDays,
            Timezone = string.IsNullOrWhiteSpace(request.Timezone) ? "auto" : request.Timezone,
            Daily = daily.ToArray()
        };
        
        var raw = await _client.GetDailyForecast(req, ct);
        if (raw is null) return null;
        
        var airQualitySection = _airQualityProcessor.Process(raw);
        var feelsLikeSection = _feelsLikeProcessor.Process(raw);
        var humiditySection = _humidityProcessor.Process(raw);
        var precipitationSection = _precipitationProcessor.Process(raw);
        var pressureSection = _pressureProcessor.Process(raw);
        var uvSection = _uvProcessor.Process(raw);
        var visibilitySection = _visibilityProcessor.Process(raw);
        
        return new DailyForecast
        {
            AirQualityDetails = airQualitySection,
            FeelsLikeDetails = feelsLikeSection,
            HumidityDetails = humiditySection,
            PrecipitationDetails = precipitationSection,
            PressureDetails = pressureSection,
            SunDetails = new SunDetails
            {
                SunriseText = raw.Daily.Sunrise.FirstOrDefault() ?? string.Empty,
                SunsetText = raw.Daily.Sunset.FirstOrDefault() ?? string.Empty,
            },
            UvDetails = uvSection,
            VisibilityDetails = visibilitySection,
            WindDetails = new WindDetails
            {
                WindSpeedMps = raw.Daily.WindSpeed10mMax?.FirstOrDefault() ?? 0,
                GustSpeedMps = raw.Daily.WindGusts10mMax?.FirstOrDefault() ?? 0,
                DirectionDegrees = raw.Daily.WindDirection10mDominant?.FirstOrDefault() ?? 0
            }
        };
    }
}
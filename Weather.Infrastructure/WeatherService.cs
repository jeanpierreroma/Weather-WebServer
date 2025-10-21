using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs;

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
    private readonly IUvIndexProcessor _uvProcessor;
    
    public WeatherService(IOpenMeteoClient client, IUvIndexProcessor uvProcessor)
    {
        _client = client;
        _uvProcessor = uvProcessor;
    }    
    
    public async Task<DailyForecast?> GetDailyForecastAsync(OpenMeteoDailyForecastRequest request, CancellationToken ct)
    {
    // 1) Гарантуємо, що витягнемо і UV, і решту «сирих» полів
    // Ensure we request UV and the other raw daily fields from the API
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

    // 2) Отримуємо сирий респонс від Open-Meteo
    // Get the raw response from the Open-Meteo API
        var raw = await _client.GetDailyForecast(req, ct);
        if (raw is null) return null;

    // 3) Обробляємо UV через окремий обробник
    // Process UV index data using the UV processor
        var uvSection = _uvProcessor.Process(raw);

    // 4) Збираємо агреговану DTO
    // Assemble the aggregated DTO to return to callers
        return new DailyForecast
        {
            Sunrise = raw.Daily.Sunrise.FirstOrDefault() ?? string.Empty,
            Sunset = raw.Daily.Sunset.FirstOrDefault() ?? string.Empty,
            
            PrecipitationSum = Convert.ToString(raw.Daily.PrecipitationSum?.FirstOrDefault()) ?? string.Empty,

            UvIndexDetails = uvSection,

            WindSpeed10mMax = Convert.ToString(raw.Daily.WindSpeed10mMax?.FirstOrDefault()) ?? string.Empty,
            WindGusts10mMax = Convert.ToString(raw.Daily.WindGusts10mMax?.FirstOrDefault()) ?? string.Empty,
            WindDirection10mDominant = Convert.ToString(raw.Daily.WindDirection10mDominant?.FirstOrDefault()) ?? string.Empty,
        };
    }
}
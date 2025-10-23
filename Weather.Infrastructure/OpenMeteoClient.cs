using System.Globalization;
using System.Text;
using Weather.Application;
using Weather.Application.OpenMeteoDTOs;
using System.Text.Json;
using Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public class OpenMeteoClient: IOpenMeteoClient 
{
    private readonly HttpClient _http;

    public OpenMeteoClient(IHttpClientFactory factory)
        => _http = factory.CreateClient("OpenMeteo");
    
    public async Task<OpenMeteoWeatherDailyForecastResponse?> GetDailyForecast(
        OpenMeteoWeatherDailyForecastRequest request,
        CancellationToken ct = default)
    {
        var daily = (request.Daily is { Count: > 0 }
            ? string.Join(",", request.Daily)
            : "uv_index_max,sunrise,sunset,precipitation_sum,wind_speed_10m_max,wind_gusts_10m_max,wind_direction_10m_dominant");

        var query = new Dictionary<string, string?>
        {
            ["latitude"]       = request.Latitude.ToString(CultureInfo.InvariantCulture),
            ["longitude"]      = request.Longitude.ToString(CultureInfo.InvariantCulture),
            ["forecast_days"]  = request.ForecastDays.ToString(CultureInfo.InvariantCulture),
            ["timezone"]       = string.IsNullOrWhiteSpace(request.Timezone) ? "auto" : request.Timezone,
            ["daily"]          = daily
        };

        var url = BuildUrl("v1/forecast", query);

        using var resp = await _http.GetAsync(url, ct);
        if (!resp.IsSuccessStatusCode)
            return null;

        await using var s = await resp.Content.ReadAsStreamAsync(ct);
        var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return await JsonSerializer.DeserializeAsync<OpenMeteoWeatherDailyForecastResponse>(s, opts, ct);
    }

    public async Task<OpenMeteoAirQualityHourlyResponse?> GetHourlyAirQuality(
        OpenMeteoAirQualityHourlyRequest request,
        CancellationToken ct = default)
    {
        var hourly = (request.Hourly is { Count: > 0 }
            ? string.Join(",", request.Hourly)
            : "european_aqi");
        
        var query = new Dictionary<string, string?>
        {
            ["latitude"]       = request.Latitude.ToString(CultureInfo.InvariantCulture),
            ["longitude"]      = request.Longitude.ToString(CultureInfo.InvariantCulture),
            ["forecast_days"]  = request.ForecastDays.ToString(CultureInfo.InvariantCulture),
            ["timezone"]       = string.IsNullOrWhiteSpace(request.Timezone) ? "auto" : request.Timezone,
            ["hourly"]         = hourly
        };
        
        var url = BuildUrl("v1/air-quality", query);
        
        using var resp = await _http.GetAsync(url, ct);
        if (!resp.IsSuccessStatusCode)
            return null;

        await using var s = await resp.Content.ReadAsStreamAsync(ct);
        var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return await JsonSerializer.DeserializeAsync<OpenMeteoAirQualityHourlyResponse>(s, opts, ct);
    }
    
    private static string BuildUrl(string path, IDictionary<string, string?> query)
    {
        var sb = new StringBuilder(path);
        var first = true;
        foreach (var kv in query)
        {
            if (string.IsNullOrEmpty(kv.Value)) continue;

            sb.Append(first ? '?' : '&');
            sb.Append(Uri.EscapeDataString(kv.Key));
            sb.Append('=');
            sb.Append(Uri.EscapeDataString(kv.Value!));
            first = false;
        }
        return sb.ToString();
    }
}
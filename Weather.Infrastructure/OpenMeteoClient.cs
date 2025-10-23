using System.Net.Http.Json;
using Weather.Application;
using System.Text.Json;
using Weather.Application.Core;
using Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Application.OpenMeteoDTOs.Settings;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;
using IHttpClientFactory = Weather.Application.Core.IHttpClientFactory;

namespace Weather.Infrastructure;

public sealed class OpenMeteoClient(IHttpClientFactory httpClientFactory)
    : NetworkServiceBase(httpClientFactory), IOpenMeteoClient 
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
    
    public async Task<OpenMeteoWeatherDailyForecastResponse?> GetDailyForecast(
        double latitude, 
        double longitude, 
        CancellationToken ct, 
        int forecastDays = 1,
        string timezone = "auto", 
        OpenMeteoSettings? settings = null, 
        params WeatherDailyField[] dailyFields)
    {
        OpenMeteoWeatherDailyForecastRequest request = OpenMeteoRequestBuilder.BuildWeatherDailyForecastRequest(latitude, longitude, forecastDays, timezone, settings,
            dailyFields);
        OpenMeteoWeatherDailyForecastResponse? response = await PerformFetchingWeatherDailyForecast(request, ct);
        return response;
    }
    
    public async Task<OpenMeteoAirQualityHourlyResponse?> GetHourlyAirQuality(
        double latitude,
        double longitude,
        CancellationToken ct,
        int forecastDays = 1,
        string timezone = "auto",
        OpenMeteoSettings? settings = null,
        params AirQualityHourlyField[] hourlyFields)
    {
        OpenMeteoAirQualityHourlyRequest request =
            OpenMeteoRequestBuilder.BuildAirQualityHourlyRequest(latitude, longitude, forecastDays, timezone, settings,
                hourlyFields);
        OpenMeteoAirQualityHourlyResponse? response = await PerformFetchingAirQualityHourly(request, ct);
        return response;
    }

    private async Task<OpenMeteoAirQualityHourlyResponse?> PerformFetchingAirQualityHourly(
        OpenMeteoAirQualityHourlyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            HttpResponseMessage response = await CreateHttpClient("air-quality-api")
                .PostAsJsonAsync(NetworkConfig.AirQuality, request, cancellationToken);

            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<OpenMeteoAirQualityHourlyResponse>(
                stream, JsonOptions, cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to fetch air quality. Error: {e.Message}", e);
        }
    }
    
    private async Task<OpenMeteoWeatherDailyForecastResponse?> PerformFetchingWeatherDailyForecast(
        OpenMeteoWeatherDailyForecastRequest request, CancellationToken cancellationToken)
    {
        try
        {
            HttpResponseMessage response = await CreateHttpClient("open-meteo-api")
                .PostAsJsonAsync(NetworkConfig.WeatherForecast, request, cancellationToken);
            response.EnsureSuccessStatusCode();
            
            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<OpenMeteoWeatherDailyForecastResponse>(stream, JsonOptions,
                cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to fetch weather forecast. Error: {e.Message}", e);
        }
    }
}
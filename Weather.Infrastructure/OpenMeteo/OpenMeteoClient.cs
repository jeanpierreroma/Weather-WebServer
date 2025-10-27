using System.Text.Json;
using Weather.Application.Abstraction;
using Weather.Application.DTOs;
using Weather.Application.Networking;
using Weather.Infrastructure.OpenMeteo.Mappers;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure.OpenMeteo;

public sealed class OpenMeteoClient(IHttpClientFactory httpClientFactory)
    : NetworkServiceBase(httpClientFactory), IForecastProvider 
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
    
    public async Task<ForecastData?> GetDailyForecast(Coordinates coordinates, ForecastOptions options, CancellationToken cancellationToken = default)
    {
        OpenMeteoWeatherDailyForecastRequest weatherDailyForecastRequest = OpenMeteoRequestBuilder.BuildWeatherDailyForecastRequest(
            coordinates.Latitude,
            coordinates.Longitude,
            options.ForecastDays,
            options.Timezone,
            options.Settings
        );
        
        OpenMeteoAirQualityHourlyRequest airQualityHourlyRequest = OpenMeteoRequestBuilder.BuildAirQualityHourlyRequest(
            coordinates.Latitude,
            coordinates.Longitude,
            options.ForecastDays,
            options.Timezone,
            options.Settings
        );
        
        Task<OpenMeteoWeatherDailyForecastResponse?> weatherDailyForecastRequestTask = PerformFetchingWeatherDailyForecast(weatherDailyForecastRequest, cancellationToken);
        Task<OpenMeteoAirQualityHourlyResponse?> airQualityHourlyRequestTask = PerformFetchingAirQualityHourlyRequest(airQualityHourlyRequest, cancellationToken);

        await Task.WhenAll(weatherDailyForecastRequestTask, airQualityHourlyRequestTask);

        OpenMeteoWeatherDailyForecastResponse? weatherDailyForecastResponse = await weatherDailyForecastRequestTask;
        OpenMeteoAirQualityHourlyResponse? airQualityHourlyResponse = await airQualityHourlyRequestTask;

        if (weatherDailyForecastResponse is null || airQualityHourlyResponse is null)
        {
            return null;
        }
        
        var daily = OpenMeteoMapper.MapOpenMeteoWeatherDailyForecastResponseToDaily(weatherDailyForecastResponse);
        var hourly = OpenMeteoMapper.MapOpenMeteoAirQualityHourlyResponseToHourly(airQualityHourlyResponse);
        
        return new ForecastData(daily, hourly);
    }
    
    private async Task<OpenMeteoWeatherDailyForecastResponse?> PerformFetchingWeatherDailyForecast(
        OpenMeteoWeatherDailyForecastRequest request, CancellationToken cancellationToken)
    {
        try
        {
            HttpClient client = CreateHttpClient("open-meteo-api");
            string url = QueryStringBuilder.Append(NetworkConfig.WeatherForecast, request);
            using HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
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

    private async Task<OpenMeteoAirQualityHourlyResponse?> PerformFetchingAirQualityHourlyRequest(
        OpenMeteoAirQualityHourlyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            HttpClient client = CreateHttpClient("air-quality-api");
            string url = QueryStringBuilder.Append(NetworkConfig.AirQuality, request);
            using HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();
            
            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<OpenMeteoAirQualityHourlyResponse>(stream, JsonOptions,
                cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to fetch air quality. Error: {e.Message}", e);
        }
    }
}
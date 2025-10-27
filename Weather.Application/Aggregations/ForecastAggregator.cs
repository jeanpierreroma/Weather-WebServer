using Weather.Application.Abstraction;
using Weather.Application.DTOs;
using Weather.Application.DTOs.Processed;
using Weather.Application.DTOs.Responses;
using Weather.Application.Processors;

namespace Weather.Application.Aggregations;

public sealed class ForecastAggregator : IForecastAggregator
{
    private readonly IAirQualityProcessor _airQualityProcessor;
    private readonly IFeelsLikeProcessor _feelsLike;
    private readonly IHumidityProcessor _humidity;
    private readonly IPrecipitationProcessor _precipitation;
    private readonly IPressureProcessor _pressure;
    private readonly IUvProcessor _uv;
    private readonly IVisibilityProcessor _visibility;

    public ForecastAggregator(
        IAirQualityProcessor airQualityProcessor,
        IFeelsLikeProcessor feelsLike,
        IHumidityProcessor humidity,
        IPrecipitationProcessor precipitation,
        IPressureProcessor pressure,
        IUvProcessor uv,
        IVisibilityProcessor visibility)
    {
        _airQualityProcessor = airQualityProcessor;
        _feelsLike = feelsLike;
        _humidity = humidity;
        _precipitation = precipitation;
        _pressure = pressure;
        _uv = uv;
        _visibility = visibility;
    }

    public async Task<ProcessedForecastSections> Aggregate(ForecastData raw, CancellationToken ct)
    {
        var airQualityTask    = Task.Run(() => _airQualityProcessor.Process(raw), ct);
        var feelsLikeTask     = Task.Run(() => _feelsLike.Process(raw), ct);
        var humidityTask      = Task.Run(() => _humidity.Process(raw), ct);
        var precipitationTask = Task.Run(() => _precipitation.Process(raw), ct);
        var pressureTask      = Task.Run(() => _pressure.Process(raw), ct);
        var uvTask            = Task.Run(() => _uv.Process(raw), ct);
        var visibilityTask    = Task.Run(() => _visibility.Process(raw), ct);

        await Task.WhenAll(feelsLikeTask, humidityTask, precipitationTask, pressureTask, uvTask, visibilityTask);

        return new ProcessedForecastSections(
            airQualityTask.Result,
            feelsLikeTask.Result,
            humidityTask.Result,
            precipitationTask.Result,
            pressureTask.Result,
            uvTask.Result,
            visibilityTask.Result
        );
    }
}
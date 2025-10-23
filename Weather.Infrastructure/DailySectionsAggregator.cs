using Weather.Application;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public sealed class DailySectionsAggregator : IDailySectionsAggregator
{
    private readonly IFeelsLikeProcessor _feelsLike;
    private readonly IHumidityProcessor _humidity;
    private readonly IPrecipitationProcessor _precipitation;
    private readonly IPressureProcessor _pressure;
    private readonly IUvProcessor _uv;
    private readonly IVisibilityProcessor _visibility;

    public DailySectionsAggregator(
        IFeelsLikeProcessor feelsLike,
        IHumidityProcessor humidity,
        IPrecipitationProcessor precipitation,
        IPressureProcessor pressure,
        IUvProcessor uv,
        IVisibilityProcessor visibility)
    {
        _feelsLike = feelsLike;
        _humidity = humidity;
        _precipitation = precipitation;
        _pressure = pressure;
        _uv = uv;
        _visibility = visibility;
    }

    public async Task<ProcessedDailySections> ProcessAsync(OpenMeteoWeatherDailyForecastResponse raw, CancellationToken ct)
    {
        var feelsLikeTask     = Task.Run(() => _feelsLike.Process(raw), ct);
        var humidityTask      = Task.Run(() => _humidity.Process(raw), ct);
        var precipitationTask = Task.Run(() => _precipitation.Process(raw), ct);
        var pressureTask      = Task.Run(() => _pressure.Process(raw), ct);
        var uvTask            = Task.Run(() => _uv.Process(raw), ct);
        var visibilityTask    = Task.Run(() => _visibility.Process(raw), ct);

        await Task.WhenAll(feelsLikeTask, humidityTask, precipitationTask, pressureTask, uvTask, visibilityTask);

        return new ProcessedDailySections(
            feelsLikeTask.Result,
            humidityTask.Result,
            precipitationTask.Result,
            pressureTask.Result,
            uvTask.Result,
            visibilityTask.Result
        );
    }
}
using Weather.Application.DTOs.Processed;

namespace Weather.Application.DTOs.Responses;

public class HourlyForecast
{
    public IReadOnlyList<HourPoint<double>> Temperature { get; init; } = [];
    public IReadOnlyList<HourPoint<double>> ApparentTemperature { get; init; } = [];
    public IReadOnlyList<HourPoint<double>> UvIndex { get; init; } = [];
    public IReadOnlyList<HourPoint<double>> Precipitation { get; init; } = [];
    public IReadOnlyList<HourPoint<double>> Visibility { get; init; } = [];
    public IReadOnlyList<HourPoint<int>> WindDirection { get; init; } = [];
    public IReadOnlyList<HourPoint<double>> WindGusts { get; init; } = [];
    public IReadOnlyList<HourPoint<double>> WindSpeed { get; init; } = [];
    public IReadOnlyList<HourPoint<int>> RelativeHumidity { get; init; } = [];
    public IReadOnlyList<HourPoint<double>> SurfacePressure { get; init; } = [];
    public IReadOnlyList<HourPoint<int>> EuropeanAqi { get; init; } = [];
}
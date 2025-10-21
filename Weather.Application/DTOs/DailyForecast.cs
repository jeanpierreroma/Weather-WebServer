namespace Weather.Application.DTOs;

public class DailyForecast
{
    public UvIndexDetails UvIndexDetails { get; init; }

    public string Sunrise { get; init; } = string.Empty;

    public string Sunset { get; init; } = string.Empty;

    public string PrecipitationSum { get; init; } = string.Empty;

    public string WindSpeed10mMax { get; init; } = string.Empty;

    public string WindGusts10mMax { get; init; } = string.Empty;

    public string WindDirection10mDominant { get; init; } = string.Empty;
}
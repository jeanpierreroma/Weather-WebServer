namespace Weather.Application.DTOs;

public class DailyForecast
{
    public AirQualityDetails AirQualityDetails { get; set; } = null!;
    public FeelsLikeDetails FeelsLikeDetails { get; set; } = null!;
    public HumidityDetails HumidityDetails { get; set; } = null!;
    public PrecipitationDetails PrecipitationDetails { get; set; } = null!;
    public PressureDetails PressureDetails { get; set; } = null!;
    public SunDetails SunDetails { get; set; } = null!;
    public UvDetails UvDetails { get; init; } = null!;
    public VisibilityDetails VisibilityDetails { get; set; } = null!;

    public WindDetails WindDetails { get; init; } = null!;
}
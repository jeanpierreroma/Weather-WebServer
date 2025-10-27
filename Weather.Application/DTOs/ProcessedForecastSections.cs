namespace Weather.Application.DTOs;

public record ProcessedForecastSections(
    AirQualityDetails AirQuality,
    FeelsLikeDetails FeelsLike,
    HumidityDetails Humidity,
    PrecipitationDetails Precipitation,
    PressureDetails Pressure,
    UvDetails Uv,
    VisibilityDetails Visibility
);
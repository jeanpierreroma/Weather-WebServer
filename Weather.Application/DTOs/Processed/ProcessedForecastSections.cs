using Weather.Application.DTOs.Responses.Details;

namespace Weather.Application.DTOs.Processed;

public record ProcessedForecastSections(
    AirQualityDetails AirQuality,
    FeelsLikeDetails FeelsLike,
    HumidityDetails Humidity,
    PrecipitationDetails Precipitation,
    PressureDetails Pressure,
    UvDetails Uv,
    VisibilityDetails Visibility,
    MoonDetails Moon
);
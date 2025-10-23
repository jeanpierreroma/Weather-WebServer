using Weather.Application.DTOs;

namespace Weather.Application;

public record ProcessedDailySections(
    FeelsLikeDetails FeelsLike,
    HumidityDetails Humidity,
    PrecipitationDetails Precipitation,
    PressureDetails Pressure,
    UvDetails Uv,
    VisibilityDetails Visibility
);
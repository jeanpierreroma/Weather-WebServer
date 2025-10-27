namespace Weather.Application.DTOs.Responses;

public record ForecastData(
    Daily Daily,
    Hourly Hourly
);

public record Daily(
    List<double?> TemperatureMean, 
    List<double?> ApparentTemperatureMean,
    List<string> Sunrise,
    List<string> Sunset,
    List<double?> UvIndexMax,
    List<double?> PrecipitationSum,
    List<double?> VisibilityMean,
    List<int?> WindDirectionDominant,
    List<double?> WindGustsMean,
    List<double?> WindSpeedMean,
    List<int?> RelativeHumidityMean,
    List<double?> SurfacePressureMean
);

public record Hourly(
    List<int> EuropeanAqi  
);
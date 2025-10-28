namespace Weather.Application.DTOs.Responses;

public record ForecastData(
    DateTime Date,
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
    List<double?> SurfacePressureMean,
    MoonSnapshotDto? MoonSnapshot  
);

public record Hourly(
    List<DateTime> Time,
    List<int> EuropeanAqi,
    List<double> Temperature, 
    List<double> ApparentTemperature,
    List<double> UvIndex,
    List<double> Precipitation,
    List<double> Visibility,
    List<int> WindDirection,
    List<double> WindGusts,
    List<double> WindSpeed,
    List<int> RelativeHumidity,
    List<double> SurfacePressure
);
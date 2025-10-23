using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public class VisibilityProcessor : IVisibilityProcessor
{
    public VisibilityDetails Process(OpenMeteoWeatherDailyForecastResponse raw)
    {
        if (raw?.WeatherDaily?.VisibilityMean == null || raw.WeatherDaily.VisibilityMean.Count == 0)
            throw new ArgumentException("visibility_mean is missing in Open-Meteo response.");
        
        double? visibilityMeanOptional = raw.WeatherDaily.VisibilityMean.FirstOrDefault();
        
        if (!visibilityMeanOptional.HasValue)
            throw new ArgumentException("visibility_mean[0] is null.");

        // meters to km
        double visibilityMeanKm = visibilityMeanOptional.Value / 1000;
        
        string summary = BuildSummary(visibilityMeanKm);

        return new VisibilityDetails
        {
            VisibilityKm = visibilityMeanKm,
            Summary = summary
        };
    }
    
    private static string BuildSummary(double visibilityKm)
    {
        if (visibilityKm <= 0.0)
            return "Видимість практично нульова (густий туман/завірюха).";

        if (visibilityKm < 1.0)
            return $"Дуже погана видимість: {visibilityKm} км (туман/мряка).";

        if (visibilityKm < 4.0)
            return $"Погана видимість: {visibilityKm} км (імовірна імла чи дощ).";

        if (visibilityKm < 10.0)
            return $"Помірна видимість: {visibilityKm} км.";

        if (visibilityKm < 20.0)
            return $"Хороша видимість: {visibilityKm} км.";

        if (visibilityKm < 40.0)
            return $"Дуже хороша видимість: {visibilityKm} км.";

        return $"Відмінна видимість: {visibilityKm} км.";
    }
}
using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;
using Weather.Domain.ValueObjects;

namespace Weather.Infrastructure;

public class UvProcessor: IUvProcessor
{
    public UvDetails Process(OpenMeteoWeatherDailyForecastResponse raw)
    {
        if (raw?.WeatherDaily?.UvIndexMax == null || raw.WeatherDaily.UvIndexMax.Count == 0)
            throw new ArgumentException("uv_index_max is missing in Open-Meteo response.");
        
        double? uvIndex = raw.WeatherDaily.UvIndexMax.FirstOrDefault();
        
        if (!uvIndex.HasValue)
            throw new ArgumentException("uv_index_max[0] is null.");
        
        var value = Math.Max(0, uvIndex.Value);
        
        value = Math.Round(value, 1, MidpointRounding.AwayFromZero);
        
        var risk = ToRisk(value);
        var riskName = RiskToString(risk);
        var summary = BuildSummary(risk);

        return new UvDetails
        {
            UvIndexValueMax = value,
            UvIndexRiskCategoryName = riskName,
            Summary = summary
        };
    }
    
    // WHO: 0–2 Low, 3–5 Moderate, 6–7 High, 8–10 Very High, 11+ Extreme
    private static UvIndexRiskCategory ToRisk(double uv) => uv switch
    {
        <= 2  => UvIndexRiskCategory.Low,
        <= 5  => UvIndexRiskCategory.Moderate,
        <= 7  => UvIndexRiskCategory.High,
        <= 10 => UvIndexRiskCategory.VeryHigh,
        _     => UvIndexRiskCategory.Extreme
    };

    private static string RiskToString(UvIndexRiskCategory risk) => risk switch
    {
        UvIndexRiskCategory.Low      => "Low",
        UvIndexRiskCategory.Moderate => "Moderate",
        UvIndexRiskCategory.High     => "High",
        UvIndexRiskCategory.VeryHigh => "Very High",
        UvIndexRiskCategory.Extreme  => "Extreme",
        _                            => "Low"
    };

    // Короткі поради за категоріями (заглушки — під заміну на локалізовані рядки)
    private static string BuildSummary(UvIndexRiskCategory risk) => risk switch
    {
        UvIndexRiskCategory.Low =>
            "Низький ризик. Можна перебувати надворі. Окуляри від сонця за потреби.",
        UvIndexRiskCategory.Moderate =>
            "Помірний ризик. Уникай полуденного сонця (10:00–16:00), користуйся SPF 30+, окулярами та кепкою.",
        UvIndexRiskCategory.High =>
            "Високий ризик. Скороти час на сонці в середині дня, захисний одяг, широкополий капелюх, окуляри, SPF 30+ кожні 2 год.",
        UvIndexRiskCategory.VeryHigh =>
            "Дуже високий ризик. За можливості уникай сонця опівдні, шукай тінь, захисний одяг та SPF 30+.",
        UvIndexRiskCategory.Extreme =>
            "Екстремальний ризик. Намагайся уникати сонця в пікові години; обов’язковий захист: одяг, капелюх, окуляри, SPF 30+.",
        _ => string.Empty
    };
}
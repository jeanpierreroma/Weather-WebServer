using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs;
using Weather.Application.OpenMeteoDTOs.Daily;
using Weather.Domain.ValueObjects;

namespace Weather.Infrastructure;

public class AirQualityProcessor : IAirQualityProcessor
{
    public AirQualityDetails Process(OpenMeteoDailyForecastResponse raw)
    {
        if (raw?.Daily?. == null || raw.Daily.UvIndexMax.Count == 0)
        {
            throw new ArgumentException("uv_index_max is missing in Open-Meteo response.");
        }
        
        double? uvIndex = raw.Daily.UvIndexMax.FirstOrDefault();
        
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
    
    private static AirQualityCategory ToCategory(double index) => index switch
    {
        <= 50  => AirQualityCategory.Good,
        <= 100 => AirQualityCategory.Satisfactory,
        <= 200 => AirQualityCategory.Moderate,
        <= 300 => AirQualityCategory.Poor,
        <= 400 => AirQualityCategory.VeryPoor,
        <= 500 => AirQualityCategory.Severe,
        _      => AirQualityCategory.Severe
    };

    private static string CategoryToString(AirQualityCategory category) => category switch
    {
        AirQualityCategory.Good => "Good",
        AirQualityCategory.Satisfactory => "Satisfactory",
        AirQualityCategory.Moderate => "Moderate",
        AirQualityCategory.Poor => "Poor",
        AirQualityCategory.VeryPoor => "Very Poor",
        AirQualityCategory.Severe => "Severe",
        _  => "Severe"
    };
    
    private static string BuildSummary(AirQualityCategory category) => category switch
    {
        AirQualityCategory.Good =>
            "Добра якість повітря: обмежень немає.",
        AirQualityCategory.Satisfactory =>
            "Задовільна: чутливим групам краще зменшити тривалі активності надворі.",
        AirQualityCategory.Moderate =>
            "Помірна: людям із захворюваннями дихальних шляхів уникайте інтенсивних навантажень на відкритому повітрі.",
        AirQualityCategory.Poor =>
            "Погана: скоротіть перебування надворі; за можливості використовуйте маску/очищувач повітря.",
        AirQualityCategory.VeryPoor =>
            "Дуже погана: залишайтеся в приміщенні, зачиніть вікна, увімкніть очищувач повітря.",
        AirQualityCategory.Severe =>
            "Небезпечна: уникайте виходів, не робіть фізичних навантажень; дотримуйтеся вказівок місцевих служб.",

        _ => "Немає даних."
    };
}
using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs;
using Weather.Application.OpenMeteoDTOs.AirQuality.Hourly;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;
using Weather.Domain.ValueObjects;

namespace Weather.Infrastructure;

public class AirQualityProcessor : IAirQualityProcessor
{
    public AirQualityDetails Process(OpenMeteoAirQualityHourlyResponse raw)
    {
        if (raw.Hourly.EuropeanAqi == null || raw.Hourly.EuropeanAqi.Count == 0)
        {
            throw new ArgumentException("european_aqi is missing in Open-Meteo response.");
        }
        
        // Average
        double aqiMean = raw.Hourly.EuropeanAqi.Average();
        
        var value = Math.Max(0, aqiMean);
        
        value = Math.Round(value, 1, MidpointRounding.AwayFromZero);
        
        var category = ToCategory(value);
        var categoryString = CategoryToString(category);
        var summary = BuildSummary(category);

        return new AirQualityDetails
        {
            AirQualityIndex = value,
            AirQualityDetailsCategoryName = categoryString,
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
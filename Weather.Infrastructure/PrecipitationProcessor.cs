using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public class PrecipitationProcessor : IPrecipitationProcessor
{
    public PrecipitationDetails Process(OpenMeteoWeatherDailyForecastResponse raw)
    {
        if (raw?.WeatherDaily?.PrecipitationSum == null || raw.WeatherDaily.PrecipitationSum.Count == 0)
            throw new ArgumentException("precipitation_sum is missing in Open-Meteo response.");
        
        double? precipitationSumOptional = raw.WeatherDaily.PrecipitationSum.FirstOrDefault();
        
        if (!precipitationSumOptional.HasValue)
            throw new ArgumentException("precipitation_sum[0] is null.");

        double precipitationSum = Math.Min(precipitationSumOptional.Value, 0);
        precipitationSum = Math.Round(precipitationSum, 2, MidpointRounding.AwayFromZero);
        
        string summary = BuildSummary(precipitationSum);

        return new PrecipitationDetails
        {
            PrecipitationDuringLast24Hours = precipitationSum,
            Summary = summary
        };
    }
    
    private static string BuildSummary(double precipitationSumMillimeters)
    {
        if (precipitationSumMillimeters == 0.0)
            return "Без опадів за останні 24 години.";

        if (precipitationSumMillimeters < 1.0)
            return $"Дуже незначні опади: {precipitationSumMillimeters} мм.";

        if (precipitationSumMillimeters < 5.0)
            return $"Невеликі опади: {precipitationSumMillimeters} мм.";

        if (precipitationSumMillimeters < 15.0)
            return $"Помірні опади: {precipitationSumMillimeters} мм.";

        if (precipitationSumMillimeters < 30.0)
            return $"Сильні опади: {precipitationSumMillimeters} мм.";

        return $"Дуже сильні (зливові) опади: {precipitationSumMillimeters} мм.";
    }
}
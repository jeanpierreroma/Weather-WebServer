using Weather.Application;
using Weather.Application.DTOs;
using Weather.Application.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure;

public class PressureProcessor : IPressureProcessor
{
    public PressureDetails Process(OpenMeteoWeatherDailyForecastResponse raw)
    {
        if (raw?.WeatherDaily?.SurfacePressureMean == null || raw.WeatherDaily.SurfacePressureMean.Count == 0)
            throw new ArgumentException("surface_pressure_mean is missing in Open-Meteo response.");
        
        double? surfacePressureMeanOptional = raw.WeatherDaily.SurfacePressureMean.FirstOrDefault();
        
        if (!surfacePressureMeanOptional.HasValue)
            throw new ArgumentException("surface_pressure_mean[0] is null.");

        double surfacePressureMean = Math.Clamp(surfacePressureMeanOptional.Value, 300, 1100);
        
        string summary = BuildSummary(surfacePressureMean);

        return new PressureDetails
        {
            PressureHpa = surfacePressureMean,
            Summary = summary
        };
    }
    
    private static string BuildSummary(double surfacePressureHpa)
    {
        const double seaLevelStandardPressureHpa = 1013.25;
        double deviationFromStandard =
            Math.Round(surfacePressureHpa - seaLevelStandardPressureHpa, 1, MidpointRounding.AwayFromZero);

        double absoluteDeviation = Math.Abs(deviationFromStandard);

        if (absoluteDeviation < 3.0)
            return $"Тиск близький до «нормального» (≈1013 гПа): {surfacePressureHpa} гПа.";

        if (absoluteDeviation < 8.0)
        {
            if (deviationFromStandard > 0)
                return $"Трохи підвищений тиск: {surfacePressureHpa} гПа (≈ +{absoluteDeviation} гПа від 1013 гПа).";
            else
                return $"Трохи понижений тиск: {surfacePressureHpa} гПа (≈ -{absoluteDeviation} гПа від 1013 гПа).";
        }

        if (absoluteDeviation < 15.0)
        {
            if (deviationFromStandard > 0)
                return $"Помітно підвищений тиск: {surfacePressureHpa} гПа. Часто асоціюється з антициклоном.";
            else
                return $"Помітно понижений тиск: {surfacePressureHpa} гПа. Часто асоціюється з циклоном.";
        }

        // Значна аномалія
        if (deviationFromStandard > 0)
            return $"Значно підвищений тиск: {surfacePressureHpa} гПа (високий антициклон).";
        else
            return $"Значно понижений тиск: {surfacePressureHpa} гПа (глибокий циклон).";
    }
}
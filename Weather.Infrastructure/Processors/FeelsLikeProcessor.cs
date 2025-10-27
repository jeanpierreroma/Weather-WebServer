using Weather.Application.DTOs;
using Weather.Application.Processors;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure.Processors;

public class FeelsLikeProcessor : IFeelsLikeProcessor
{
    public FeelsLikeDetails Process(ForecastData raw)
    {
        if (raw.Daily.ApparentTemperatureMean == null || raw.Daily.ApparentTemperatureMean.Count == 0)
        {
            throw new ArgumentException("apparent_temperature_mean is missing in Open-Meteo response.");
        }
        if (raw.Daily.TemperatureMean == null || raw.Daily.TemperatureMean.Count == 0)
        {
            throw new ArgumentException("temperature_2m_mean is missing in Open-Meteo response.");
        }
        
        double? temperatureMeanOptional = raw.Daily.TemperatureMean.FirstOrDefault();
        
        if (!temperatureMeanOptional.HasValue)
            throw new ArgumentException("temperature_2m_mean[0] is null.");
        
        double? apparentTemperatureOptional = raw.Daily.ApparentTemperatureMean.FirstOrDefault();
        
        if (!apparentTemperatureOptional.HasValue)
            throw new ArgumentException("apparent_temperature_mean[0] is null.");
        
        double temperatureMean = Math.Round(temperatureMeanOptional.Value, 1, MidpointRounding.AwayFromZero);

        double apparentTemperature = Math.Round(apparentTemperatureOptional.Value, 1, MidpointRounding.AwayFromZero);
        
        string summary = BuildSummary(temperatureMean, apparentTemperature);

        return new FeelsLikeDetails
        {
            FeelsLikeTemperature = apparentTemperature,
            Summary = summary
        };
    }
    
    private static string BuildSummary(double temperatureMean, double apparentTemperature)
    {
        double temperatureDifference = Math.Round(apparentTemperature - temperatureMean, 1, MidpointRounding.AwayFromZero);

        // Пороги-заглушки:
        // |Δ| < 0.5 → відчувається майже як фактична
        // 0.5–2.0   → трохи тепліше/холодніше
        // 2.0–5.0   → помітно тепліше/холодніше
        // ≥ 5.0     → суттєво тепліше/холодніше
        double absoluteDifference = Math.Abs(temperatureDifference);

        if (absoluteDifference < 0.5)
        {
            return $"Відчувається майже так само, як фактична температура: {apparentTemperature}°C.";
        }

        if (absoluteDifference < 2.0)
        {
            if (temperatureDifference > 0)
                return $"Відчувається трохи тепліше за середню: {apparentTemperature}°C (≈ +{absoluteDifference}°C).";
            else
                return $"Відчувається трохи холодніше за середню: {apparentTemperature}°C (≈ -{absoluteDifference}°C).";
        }

        if (absoluteDifference < 5.0)
        {
            if (temperatureDifference > 0)
                return $"Відчувається помітно тепліше: {apparentTemperature}°C (≈ +{absoluteDifference}°C).";
            else
                return $"Відчувається помітно холодніше: {apparentTemperature}°C (≈ -{absoluteDifference}°C).";
        }

        // Суттєва різниця
        if (temperatureDifference > 0)
            return $"Відчувається значно тепліше за фактичну: {apparentTemperature}°C (≈ +{absoluteDifference}°C).";
        else
            return $"Відчувається значно холодніше за фактичну: {apparentTemperature}°C (≈ -{absoluteDifference}°C).";
    }
}
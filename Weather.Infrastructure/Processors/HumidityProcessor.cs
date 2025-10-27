using Weather.Application.DTOs;
using Weather.Application.DTOs.Responses;
using Weather.Application.DTOs.Responses.Details;
using Weather.Application.Processors;
using Weather.Infrastructure.OpenMeteo.OpenMeteoDTOs.Weather.Daily;

namespace Weather.Infrastructure.Processors;

public class HumidityProcessor : IHumidityProcessor
{
    public HumidityDetails Process(ForecastData raw)
    {
        if (raw?.Daily?.RelativeHumidityMean == null || raw.Daily.RelativeHumidityMean.Count == 0)
            throw new ArgumentException("relative_humidity_2m_mean is missing in Open-Meteo response.");
        
        int? humidityOptional = raw.Daily.RelativeHumidityMean.FirstOrDefault();
        
        if (!humidityOptional.HasValue)
            throw new ArgumentException("relative_humidity_2m_mean[0] is null.");

        var humidity = Math.Clamp(humidityOptional.Value, 0, 100);
        
        string summary = BuildSummary(humidity);

        return new HumidityDetails
        {
            HumidityPercent = humidity,
            Summary = summary
        };
    }
    
    private static string BuildSummary(int humidityPercent)
    {
        if (humidityPercent < 20)
            return $"Дуже сухо: {humidityPercent}%. Можливий дискомфорт (сухість очей та шкіри).";
        if (humidityPercent < 30)
            return $"Сухо: {humidityPercent}%. Для комфорту може знадобитися зволоження повітря.";
        if (humidityPercent <= 60)
            return $"Комфортна вологість: {humidityPercent}%. Оптимально для більшості людей у приміщенні.";
        if (humidityPercent <= 70)
            return $"Трохи волого: {humidityPercent}%. Може посилюватися відчуття задухи в спеку.";
        if (humidityPercent <= 85)
            return $"Висока вологість: {humidityPercent}%. Можливі конденсат і задуха — бажано провітрювання.";
        
        return $"Дуже висока вологість: {humidityPercent}%. Імовірна задуха та конденсат; рекомендується провітрювання або осушення.";
    }
}
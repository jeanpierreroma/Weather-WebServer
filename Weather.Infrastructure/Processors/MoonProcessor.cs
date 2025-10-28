using System.Globalization;
using Weather.Application.DTOs.Responses;
using Weather.Application.DTOs.Responses.Details;
using Weather.Application.Processors;

namespace Weather.Infrastructure.Processors;

public class MoonProcessor : IMoonProcessor
{
    private readonly TimeOnly _hardcodedRise;
    private readonly TimeOnly _hardcodedSet;
    private readonly DateTimeKind _kind;
    
    public MoonProcessor() : this(new TimeOnly(6, 12), new TimeOnly(18, 3)) { }

    public MoonProcessor(TimeOnly hardcodedRise, TimeOnly hardcodedSet, DateTimeKind kind = DateTimeKind.Utc)
    {
        _hardcodedRise = hardcodedRise;
        _hardcodedSet  = hardcodedSet;
        _kind = kind;
    }
    public MoonDetails Process(ForecastData raw)
    {
        if (raw.Daily.MoonSnapshot is null)
        {
            throw new ArgumentException("moon snapshot is missing in response.");
        }
        
        var date = ExtractDateOnly(raw.Daily.MoonSnapshot.Time);

        var moonrise = date.ToDateTime(_hardcodedRise, _kind);
        var moonset  = date.ToDateTime(_hardcodedSet,  _kind);

        double illuminationPercent = Math.Clamp(raw.Daily.MoonSnapshot.Phase, 0, 100);
        const double synodicMonth = 29.530588853;      
        double half = synodicMonth / 2.0;
        bool waxing = raw.Daily.MoonSnapshot.Age < half;              

        string phaseName = PhaseName(illuminationPercent, waxing);
        int nextFull = DaysUntilFullMoon(raw.Daily.MoonSnapshot.Age);

        return new MoonDetails
        {
            PhaseName = phaseName,
            MoonUrl = raw.Daily.MoonSnapshot.ImageUrl,
            IlluminationPercent = illuminationPercent,
            Moonrise = moonrise,
            Moonset = moonset,
            NextFullMoonDays = nextFull,
            Distance = raw.Daily.MoonSnapshot.Distance
        };
    }
    
    private static DateOnly ExtractDateOnly(string timestamp)
    {
        if (DateTimeOffset.TryParse(
                timestamp, 
                formatProvider: null,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out var dto)
        )
        {
            return DateOnly.FromDateTime(dto.UtcDateTime);
        }

        if (DateTime.TryParse(timestamp, out var dt))
        {
            return DateOnly.FromDateTime(dt);
        }

        if (timestamp.Length >= 10 
            && DateOnly.TryParseExact(timestamp[..10], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var d))
        {
            return d;
        }

        return DateOnly.FromDateTime(DateTime.UtcNow);
    }

    private static int DaysUntilFullMoon(double age)
    {
        const double synodicMonth = 29.530588853;
        double half = synodicMonth / 2.0;

        double days = age <= half 
            ? (half - age) 
            : (synodicMonth - age + half);

        return Math.Max(0, (int)Math.Round(days, MidpointRounding.AwayFromZero));
    }

    private static string PhaseName(double illuminationPercent, bool waxing)
    {
        const double newTol  = 1.0; 
        const double fullTol = 1.0; 
        const double qTol    = 3.0; 

        if (illuminationPercent <= newTol)
        {
            return "Молодик";
        }
        if (illuminationPercent >= 100.0 - fullTol)
        {
            return "Повня";
        }
        if (Math.Abs(illuminationPercent - 50.0) <= qTol)
        {
            return waxing ? "Перша чверть" : "Остання чверть";
        }

        if (illuminationPercent < 50.0)
        {
            return waxing ? "Зростаючий серп" : "Спадний серп";
        }
        else
        {
            return waxing ? "Зростаючий опуклий" : "Спадний опуклий";
        }
    }
}
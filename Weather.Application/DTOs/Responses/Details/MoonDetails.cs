namespace Weather.Application.DTOs.Responses.Details;

public class MoonDetails
{
    public string PhaseName { get; set; } = null!;
    public string MoonUrl { get; set; } = null!;
    public double IlluminationPercent { get; set; }
    public DateTime Moonset { get; set; }
    public DateTime Moonrise { get; set; }
    public int NextFullMoonDays { get; set; }
    public double Distance { get; set; }
}
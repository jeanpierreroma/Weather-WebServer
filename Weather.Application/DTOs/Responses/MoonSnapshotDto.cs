namespace Weather.Application.DTOs.Responses;

public class MoonSnapshotDto
{
    public required string ImageUrl { get; init; }
    public required string Time { get; init; }
    public double Phase { get; init; }
    public double Obscuration { get; init; }
    public double Age { get; init; }
    public double Diameter { get; init; }
    public double Distance { get; init; }
    public double J2000Ra { get; init; }
    public double J2000Dec { get; init; }
    public double SubsolarLon { get; init; }
    public double SubsolarLat { get; init; }
    public double SubearthLon { get; init; }
    public double SubearthLat { get; init; }
    public double Posangle { get; init; }
}
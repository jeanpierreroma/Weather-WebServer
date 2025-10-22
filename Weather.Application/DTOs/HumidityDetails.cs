namespace Weather.Application.DTOs;

public class HumidityDetails
{
    // 0 ... 1
    public double HumidityPercent { get; set; }
    public string Summary { get; set; } = null!;
}
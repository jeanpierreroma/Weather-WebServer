namespace Weather.Application.DTOs;

public class HumidityDetails
{
    // 0 ... 100
    public int HumidityPercent { get; set; }
    public string Summary { get; set; } = null!;
}
namespace Weather.Application.DTOs.Responses.Details;

public class HumidityDetails
{
    // 0 ... 100
    public int HumidityPercent { get; set; }
    public string Summary { get; set; } = null!;
}
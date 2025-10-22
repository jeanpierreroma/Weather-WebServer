namespace Weather.Application.DTOs;

public class AirQualityDetails
{
    public double AirQualityIndex { get; set; }
    public string AirQualityDetailsCategoryName { get; set; } = null!;
    public string Summary { get; set; } = null!;
}